// ORIGINAL CODE. Evidence: ItemGear*Table (Template, UpgradeInfo/Data/Synergy, Conversion, ResetOpt, Succession,
// WearCondition), Emblem*Table, Essentials*Table, BlessingInfoTable, SetBonusTable, StatOption*Table, ItemOptionRate.
using System;
using System.Collections.Generic;
using LoaReconstruction.Core;

namespace LoaReconstruction.Equipment
{
    public sealed class StatOption { public string StatKey; public double Value; }

    public sealed class GearInstance
    {
        public long InstanceId; public int TemplateId; public int UpgradeLevel;
        public List<StatOption> Options = new List<StatOption>();
    }

    public interface IEnhanceRules
    {
        double SuccessRate(GearInstance g);   // EmblemEnhanceRateTooltip / ItemOptionRate show rates; values unknown
        ItemStack Cost(GearInstance g);
        bool LosesLevelOnFailure(GearInstance g); // unknown whether failure penalizes
    }

    public static class EnhanceService
    {
        public static bool TryEnhance(GearInstance g, Wallet wallet, IEnhanceRules rules, IRandom rng)
        {
            if (!wallet.TrySpend(rules.Cost(g))) return false;
            if (rng.NextDouble() < rules.SuccessRate(g)) { g.UpgradeLevel++; return true; }
            if (rules.LosesLevelOnFailure(g) && g.UpgradeLevel > 0) g.UpgradeLevel--;
            return false;
        }
    }

    /// <summary>Random stat options drawn from a pool (StatOptionPoolTable). Weights unknown, injected.</summary>
    public sealed class StatOptionPool
    {
        public readonly List<string> Keys = new List<string>();
        public readonly List<double> Weights = new List<double>();
        public StatOption Roll(IRandom rng, Func<string, double> valueFor)
        {
            string k = Keys[WeightedPick.Index(Weights, rng)];
            return new StatOption { StatKey = k, Value = valueFor(k) };
        }
    }

    /// <summary>Set/synergy bonuses (SetBonusTable, EmblemSynergyTable, EssentialsSynergyTable):
    /// bonus tiers unlocked by the number of matching pieces. Thresholds and values come from data.</summary>
    public sealed class SynergyEvaluator
    {
        public sealed class Tier { public int RequiredCount; public List<StatOption> Bonus = new List<StatOption>(); }
        private readonly Dictionary<int, List<Tier>> _bySet = new Dictionary<int, List<Tier>>();
        public void AddTier(int setId, Tier t)
        {
            List<Tier> l; if (!_bySet.TryGetValue(setId, out l)) _bySet[setId] = l = new List<Tier>();
            l.Add(t);
        }
        public List<StatOption> Evaluate(int setId, int equippedCount)
        {
            var result = new List<StatOption>(); List<Tier> l;
            if (_bySet.TryGetValue(setId, out l))
                foreach (var t in l) if (equippedCount >= t.RequiredCount) result.AddRange(t.Bonus);
            return result;
        }
    }
}

// ORIGINAL CODE. Evidence: SummonInfoTable, SummonPoolTable, SummonLevelUpTable, SummonStressTable, ShopSummonTable,
// ShopSummonCostTable, PickTable, GachaLevelBar, GachaStressInfoPopUp, GachaViewPolymorph/Skill/Spirit, GachaRateInfo.
using System;
using System.Collections.Generic;
using LoaReconstruction.Core;

namespace LoaReconstruction.Summon
{
    // VERIFIED: three gacha views exist (GachaViewPolymorph, GachaViewSkill, GachaViewSpirit).
    public enum SummonCategory { Polymorph, Skill, Spirit }

    public sealed class SummonEntry { public int ItemId; public int Grade; public double Weight; }

    public sealed class SummonPool
    {
        public SummonCategory Category;
        public List<SummonEntry> Entries = new List<SummonEntry>();
    }

    /// <summary>HYPOTHESIS (not proven): SummonStressTable / GachaStressInfoPopUp suggest a pity-like counter.
    /// Default implementation never forces anything.</summary>
    public interface IPityRule { bool ShouldForce(int rollsSinceLastTopGrade, out int forcedGrade); }
    public sealed class NoPity : IPityRule
    {
        public bool ShouldForce(int rolls, out int forcedGrade) { forcedGrade = 0; return false; }
    }

    public sealed class SummonService
    {
        private readonly IPityRule _pity; private int _sinceTop;
        public SummonService(IPityRule pity) { _pity = pity ?? new NoPity(); }

        public List<SummonEntry> Roll(SummonPool pool, int count, int topGrade, IRandom rng)
        {
            var results = new List<SummonEntry>();
            var weights = new List<double>(); foreach (var e in pool.Entries) weights.Add(e.Weight);
            for (int i = 0; i < count; i++)
            {
                int forced; SummonEntry pick;
                if (_pity.ShouldForce(_sinceTop, out forced)) pick = FirstOfGrade(pool, forced, rng) ?? pool.Entries[WeightedPick.Index(weights, rng)];
                else pick = pool.Entries[WeightedPick.Index(weights, rng)];
                _sinceTop = pick.Grade >= topGrade ? 0 : _sinceTop + 1;
                results.Add(pick);
            }
            return results;
        }

        private static SummonEntry FirstOfGrade(SummonPool p, int grade, IRandom rng)
        {
            var l = p.Entries.FindAll(e => e.Grade == grade);
            return l.Count == 0 ? null : l[rng.Next(l.Count)];
        }
    }
}

// ORIGINAL CODE. Evidence: PolymorphTable, PolymorphBackTable, SpiritTable, SpiritLeaderEffect, PetUnit,
// GrowthSpecializeTreeTable, SpecializeTreeViewLock, PolymorphTranscendence* classes, PolymorphSynthesisRateInfo.
using System;
using System.Collections.Generic;
using LoaReconstruction.Core;

namespace LoaReconstruction.Characters
{
    public sealed class PolymorphInstance
    {
        public int TemplateId; public int Level; public int TranscendenceStars;
    }

    // Reward type names come from PolymorphTranscendenceReward_* subclasses (VERIFIED names).
    public enum TranscendenceRewardKind { DecoTitle, PolymorphBack, PolymorphGear, ProfileFrame, ProfileMotionPortrait, TranscendenceSkill }

    public interface ISynthesisRules
    {
        // PolymorphSynthesisRateInfo proves that synthesis has displayed rates; the numbers and inputs are unknown.
        double SuccessRate(IReadOnlyList<PolymorphInstance> inputs);
        int ResultTemplate(IReadOnlyList<PolymorphInstance> inputs, bool success);
    }

    public static class SynthesisService
    {
        public static bool Try(IReadOnlyList<PolymorphInstance> inputs, ISynthesisRules rules, IRandom rng, out PolymorphInstance result)
        {
            bool ok = rng.NextDouble() < rules.SuccessRate(inputs);
            result = new PolymorphInstance { TemplateId = rules.ResultTemplate(inputs, ok), Level = 1 };
            return ok;
        }
    }

    /// <summary>Prerequisite tree. Real node layout/costs are unknown; this only models the unlock logic
    /// suggested by SpecializeTreeViewLock / SpecializeConditionLabel.</summary>
    public sealed class SpecializeTree
    {
        public sealed class Node { public int Id; public List<int> Requires = new List<int>(); public ItemStack Cost; }
        private readonly Dictionary<int, Node> _nodes = new Dictionary<int, Node>();
        private readonly HashSet<int> _unlocked = new HashSet<int>();
        public void AddNode(Node n) { _nodes[n.Id] = n; }
        public bool IsUnlocked(int id) { return _unlocked.Contains(id); }
        public bool CanUnlock(int id)
        {
            Node n; if (!_nodes.TryGetValue(id, out n) || _unlocked.Contains(id)) return false;
            foreach (var r in n.Requires) if (!_unlocked.Contains(r)) return false;
            return true;
        }
        public bool TryUnlock(int id, Wallet w)
        {
            if (!CanUnlock(id) || !w.TrySpend(_nodes[id].Cost)) return false;
            _unlocked.Add(id); return true;
        }
    }
}

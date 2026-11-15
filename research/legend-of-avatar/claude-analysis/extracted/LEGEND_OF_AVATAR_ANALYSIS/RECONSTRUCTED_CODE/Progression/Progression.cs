// ORIGINAL CODE. Evidence: ChapterTable, StageTable, StagePenaltyTable, AutoBattleRewardGroup, AutoBattleRewardADPopup,
// Dungeon*StageTable, DungeonTower*Table, GrowthLevelUp/Train/HardTrain/Amplify/Specialize/UpgradeTable.
using System;
using System.Collections.Generic;
using LoaReconstruction.Core;

namespace LoaReconstruction.Progression
{
    public struct StageProgress { public int Chapter; public int Stage; }

    // Dungeon names come from Dungeon*StageTable class names (VERIFIED names).
    public enum ResourceDungeon { Accessory, Emblem, Event, Gacha, Halidom, Relic, Rune, Upgrade, Vessel }
    public enum TowerMode { Basic, Combo, Counter, Skill }

    public interface IOfflineRewardRules
    {
        TimeSpan MaxAccrual { get; }                       // cap unknown
        RewardBundle RewardFor(StageProgress s, TimeSpan accrued); // rate formula unknown (AutoBattleRewardGroup)
    }

    public static class OfflineRewards
    {
        public static RewardBundle Claim(DateTime lastClaimUtc, DateTime nowUtc, StageProgress stage, IOfflineRewardRules rules)
        {
            var accrued = nowUtc - lastClaimUtc;
            if (accrued < TimeSpan.Zero) accrued = TimeSpan.Zero;
            if (accrued > rules.MaxAccrual) accrued = rules.MaxAccrual;
            return rules.RewardFor(stage, accrued);
        }
    }

    /// <summary>Per-day entry limiter for dungeons. Daily limits and reset time are unknown, so injected.</summary>
    public sealed class DailyEntryTracker
    {
        private readonly Func<int> _limit; private readonly Func<DateTime> _dayKey;
        private DateTime _currentDay; private int _used;
        public DailyEntryTracker(Func<int> limit, Func<DateTime> dayKey) { _limit = limit; _dayKey = dayKey; }
        public bool TryEnter()
        {
            var day = _dayKey().Date;
            if (day != _currentDay) { _currentDay = day; _used = 0; }
            if (_used >= _limit()) return false;
            _used++; return true;
        }
    }

    public sealed class LevelCurve
    {
        private readonly List<long> _expToNext;     // thresholds supplied from data (unknown here)
        public LevelCurve(IEnumerable<long> expToNext) { _expToNext = new List<long>(expToNext); }
        public int Apply(int level, ref long exp)
        {
            while (level - 1 < _expToNext.Count && exp >= _expToNext[level - 1]) { exp -= _expToNext[level - 1]; level++; }
            return level;
        }
    }
}

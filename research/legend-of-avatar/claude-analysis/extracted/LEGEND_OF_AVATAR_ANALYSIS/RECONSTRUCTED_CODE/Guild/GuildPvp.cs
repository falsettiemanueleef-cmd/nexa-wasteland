// ORIGINAL CODE. Evidence: GuildWar5vs5GameMode, GuildWarReplaySlot_BuffWar / _LeaderWar, GuildWarTournamantGameMode,
// GuildWarScheduleTable, GuildWarSeasonTable, GuildRaid*Table, GuildBossRewardTable, ArenaGameMode, RankingInfoTable,
// RankingRewardTable, GuildLevelUpTable, ChatManager.
using System;
using System.Collections.Generic;
using LoaReconstruction.Core;

namespace LoaReconstruction.Guild
{
    // VERIFIED names from class names; the rules of each mode are NOT recovered.
    public enum GuildWarMode { LeaderWar, BuffWar, Tournament }

    public sealed class SeasonWindow
    {
        public DateTime StartUtc, EndUtc;
        public bool IsActive(DateTime nowUtc) { return nowUtc >= StartUtc && nowUtc < EndUtc; }
    }

    /// <summary>Rank-range -> reward lookup (RankingRewardTable / GuildBossRewardTable pattern).
    /// Ranges and rewards are data and unknown here.</summary>
    public sealed class RankRewardTable
    {
        private struct Band { public int From, To; public RewardBundle Reward; }
        private readonly List<Band> _bands = new List<Band>();
        public void AddBand(int fromRank, int toRank, RewardBundle reward) { _bands.Add(new Band { From = fromRank, To = toRank, Reward = reward }); }
        public RewardBundle For(int rank)
        {
            foreach (var b in _bands) if (rank >= b.From && rank <= b.To) return b.Reward;
            return new RewardBundle();
        }
    }

    public sealed class GuildState
    {
        public int Level; public long Exp;
        public HashSet<long> Members = new HashSet<long>();
        public bool AddMember(long playerId, int maxMembers) // member cap not recovered -> caller supplies it
        {
            if (Members.Count >= maxMembers) return false; return Members.Add(playerId);
        }
    }
}

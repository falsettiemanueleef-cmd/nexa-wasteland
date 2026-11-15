// ORIGINAL CODE. Structure inferred from class names: BattleSimulator, BattleFormation, BattleEvent,
// BattleRecord, BattleTime, BattleResultBase, MonsterTable, SkillTable, SkillEffect*Table, SkillConditionTable.
// Every formula is injected through IBattleRules; the game's real formulas are NOT recovered.
using System;
using System.Collections.Generic;
using LoaReconstruction.Core;

namespace LoaReconstruction.Battle
{
    public enum Team { Player, Enemy }

    // Names taken from the SkillEffect*Table class names found in the APK (VERIFIED names, unknown semantics).
    public enum SkillEffectKind { Attack, Barrier, Buff, Extra, FixedDamage, Heal, Installed, Reflect, Shield }

    public sealed class BattleUnit
    {
        public int Id; public Team Team; public string Name;
        public double Hp; public double MaxHp;
        public Dictionary<string, double> Stats = new Dictionary<string, double>(); // keys unknown
        public int NextActionTick;
        public bool Alive { get { return Hp > 0; } }
    }

    public struct BattleEventRecord
    {
        public int Tick, SourceId, TargetId; public string Kind; public double Amount;
    }

    public sealed class BattleResult
    {
        public Team? Winner;                 // null = timeout
        public int Ticks;
        public List<BattleEventRecord> Log = new List<BattleEventRecord>();
    }

    public interface IBattleRules
    {
        int MaxTicks { get; }
        int InitialActionDelay(BattleUnit u);
        int ActionInterval(BattleUnit u);
        BattleUnit SelectTarget(BattleUnit actor, IReadOnlyList<BattleUnit> opponents, IRandom rng);
        double ComputeDamage(BattleUnit attacker, BattleUnit defender, IRandom rng, out bool crit);
    }

    /// <summary>Default rules: every member throws, because nothing was recovered.</summary>
    public sealed class UnrecoveredRules : IBattleRules
    {
        public int MaxTicks { get { throw new NotRecoveredException("battle time limit"); } }
        public int InitialActionDelay(BattleUnit u) { throw new NotRecoveredException("initial action delay"); }
        public int ActionInterval(BattleUnit u) { throw new NotRecoveredException("speed/attack interval formula"); }
        public BattleUnit SelectTarget(BattleUnit a, IReadOnlyList<BattleUnit> o, IRandom r) { throw new NotRecoveredException("targeting rule"); }
        public double ComputeDamage(BattleUnit a, BattleUnit d, IRandom r, out bool crit) { throw new NotRecoveredException("damage formula"); }
    }

    /// <summary>Deterministic tick-based auto-battle loop (seeded RNG makes a replay reproducible).</summary>
    public sealed class BattleSimulator
    {
        private readonly IBattleRules _rules;
        public BattleSimulator(IBattleRules rules) { _rules = rules; }

        public BattleResult Run(IList<BattleUnit> units, IRandom rng)
        {
            var res = new BattleResult();
            foreach (var u in units) u.NextActionTick = _rules.InitialActionDelay(u);
            int max = _rules.MaxTicks;
            for (int tick = 0; tick < max; tick++)
            {
                res.Ticks = tick;
                foreach (var actor in units)
                {
                    if (!actor.Alive || actor.NextActionTick > tick) continue;
                    var opp = Opponents(units, actor.Team);
                    if (opp.Count == 0) { res.Winner = actor.Team; return res; }
                    var target = _rules.SelectTarget(actor, opp, rng);
                    bool crit;
                    double dmg = _rules.ComputeDamage(actor, target, rng, out crit);
                    target.Hp -= dmg;
                    res.Log.Add(new BattleEventRecord { Tick = tick, SourceId = actor.Id, TargetId = target.Id,
                        Kind = crit ? "critical" : "hit", Amount = dmg });
                    actor.NextActionTick = tick + _rules.ActionInterval(actor);
                }
                if (Opponents(units, Team.Player).Count == 0) { res.Winner = Team.Player; return res; }
                if (Opponents(units, Team.Enemy).Count == 0) { res.Winner = Team.Enemy; return res; }
            }
            return res; // timeout, Winner == null
        }

        private static List<BattleUnit> Opponents(IList<BattleUnit> units, Team me)
        {
            var l = new List<BattleUnit>();
            foreach (var u in units) if (u.Alive && u.Team != me) l.Add(u);
            return l;
        }
    }
}

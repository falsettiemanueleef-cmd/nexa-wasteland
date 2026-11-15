// ORIGINAL CODE written for this project. It is NOT recovered game source.
// Pure C# (no UnityEngine dependency) so it can be dropped into a Unity project.
using System;
using System.Collections.Generic;

namespace LoaReconstruction.Core
{
    /// <summary>Thrown where the real value/behaviour could not be recovered from the APK.</summary>
    public sealed class NotRecoveredException : Exception
    {
        public NotRecoveredException(string what) : base("Not recovered from APK: " + what) { }
    }

    public interface ITableRow { int Id { get; } }

    /// <summary>Generic id-keyed table. Column schemas of the real *Table classes are NOT known.</summary>
    public sealed class GameDataTable<T> where T : ITableRow
    {
        private readonly Dictionary<int, T> _rows = new Dictionary<int, T>();
        public int Count { get { return _rows.Count; } }
        public IEnumerable<T> All { get { return _rows.Values; } }
        public void Add(T row) { _rows[row.Id] = row; }
        public bool TryGet(int id, out T row) { return _rows.TryGetValue(id, out row); }
        public T Get(int id)
        {
            T row;
            if (!_rows.TryGetValue(id, out row)) throw new KeyNotFoundException("Row " + id);
            return row;
        }
    }

    public interface IRandom { double NextDouble(); int Next(int maxExclusive); }

    public sealed class SeededRandom : IRandom
    {
        private readonly Random _r;
        public SeededRandom(int seed) { _r = new Random(seed); }
        public double NextDouble() { return _r.NextDouble(); }
        public int Next(int maxExclusive) { return _r.Next(maxExclusive); }
    }

    public struct ItemStack
    {
        public int ItemId; public long Amount;
        public ItemStack(int itemId, long amount) { ItemId = itemId; Amount = amount; }
    }

    public sealed class RewardBundle
    {
        public readonly List<ItemStack> Items = new List<ItemStack>();
    }

    /// <summary>Local mirror of balances. In the real game currency appears to be server-side
    /// (class names CurrencyDto / CurrencyEndpoint), so treat this as a client-side cache.</summary>
    public sealed class Wallet
    {
        private readonly Dictionary<int, long> _b = new Dictionary<int, long>();
        public long Get(int itemId) { long v; return _b.TryGetValue(itemId, out v) ? v : 0; }
        public void Add(int itemId, long amount) { _b[itemId] = Get(itemId) + amount; }
        public bool TrySpend(ItemStack cost)
        {
            if (Get(cost.ItemId) < cost.Amount) return false;
            _b[cost.ItemId] = Get(cost.ItemId) - cost.Amount; return true;
        }
    }

    /// <summary>Weighted pick. Weights come from data tables (SummonPoolTable, StatOptionPoolTable...) whose values are unknown.</summary>
    public static class WeightedPick
    {
        public static int Index(IList<double> weights, IRandom rng)
        {
            double total = 0; for (int i = 0; i < weights.Count; i++) total += weights[i];
            if (total <= 0) throw new ArgumentException("weights must sum to > 0");
            double roll = rng.NextDouble() * total, acc = 0;
            for (int i = 0; i < weights.Count; i++) { acc += weights[i]; if (roll < acc) return i; }
            return weights.Count - 1;
        }
    }
}

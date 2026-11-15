// ORIGINAL CODE. Evidence: BenefitShopSlot_{Free,IAP,IAP_RepeatBuy,Monthly,RemoveAd}, ShopProductTable, ShopBenefitTable,
// ShopRepeatItemTable, ShopProductResetCycleComponent, ProductAvailableConditionHelper, VipTable, AdBuffTable,
// AdsManager, QuestTable/QuestDailyRewardTable/QuestAchievementRewardTable/QuestRoadmapTable/QuestPassTable, CurrencyEndpoint.
using System;
using System.Collections.Generic;
using LoaReconstruction.Core;

namespace LoaReconstruction.Economy
{
    // Product kinds from BenefitShopSlot_* class names (VERIFIED names).
    public enum ProductKind { Free, Iap, IapRepeatBuy, Monthly, RemoveAd }

    public sealed class ShopProduct
    {
        public int Id; public ProductKind Kind;
        public ItemStack? SoftCost;      // null for IAP/free
        public RewardBundle Reward = new RewardBundle();
        public int PurchaseLimit;        // reset cycle and limits are unknown -> data supplied
    }

    /// <summary>Real-money purchases need the platform store and the game server; only an interface is provided.</summary>
    public interface IStoreGateway { bool TryPurchase(ShopProduct product); }

    public sealed class PurchaseService
    {
        private readonly Wallet _wallet; private readonly IStoreGateway _store;
        private readonly Dictionary<int, int> _count = new Dictionary<int, int>();
        public PurchaseService(Wallet wallet, IStoreGateway store) { _wallet = wallet; _store = store; }

        public bool Buy(ShopProduct p)
        {
            int n; _count.TryGetValue(p.Id, out n);
            if (p.PurchaseLimit > 0 && n >= p.PurchaseLimit) return false;
            bool paid = p.Kind == ProductKind.Free
                || (p.SoftCost.HasValue && _wallet.TrySpend(p.SoftCost.Value))
                || ((p.Kind == ProductKind.Iap || p.Kind == ProductKind.IapRepeatBuy || p.Kind == ProductKind.Monthly || p.Kind == ProductKind.RemoveAd) && _store != null && _store.TryPurchase(p));
            if (!paid) return false;
            foreach (var s in p.Reward.Items) _wallet.Add(s.ItemId, s.Amount);
            _count[p.Id] = n + 1; return true;
        }
    }

    public sealed class QuestProgress
    {
        public int QuestId; public long Current; public long Target; public bool Claimed;
        public bool Complete { get { return Current >= Target; } }
        public void Add(long amount) { Current = Math.Min(Target, Current + amount); }
        public RewardBundle Claim(RewardBundle reward)
        {
            if (!Complete || Claimed) return new RewardBundle();
            Claimed = true; return reward;
        }
    }
}

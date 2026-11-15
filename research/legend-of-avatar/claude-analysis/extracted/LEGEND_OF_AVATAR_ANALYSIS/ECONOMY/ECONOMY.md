# Economia, negozio, acquisti, quest

Etichette: **VERIFICATO** (letto dal file), **RICOSTRUITO** (dedotto da evidenze), **IPOTESI**, **NON RECUPERABILE**.

## Funzionamento
- VERIFICATO: tipi di slot nel negozio dei benefici: Free, IAP, IAP_RepeatBuy, Monthly (anche Cargo/Mine/Train) e RemoveAd.
- RICOSTRUITO: prodotti e benefici (`ShopProductTable`, `ShopBenefitTable`, `ShopRepeatItemTable`), cicli di reset (`ShopProductResetCycleComponent`), condizioni di disponibilità (`ProductAvailableConditionHelper`), VIP (`VipTable`), buff da pubblicità (`AdBuffTable`, `AdsManager`) e Battle Pass (`QuestPassTable`, `BattlePassInfoPopup`).
- RICOSTRUITO: quest in cinque forme: Quest, Daily, Achievement, Roadmap, Pass; più Attendance.
- RICOSTRUITO: `CurrencyDto`/`CurrencyEndpoint`: le valute sembrano gestite via API server.
- SDK nell'APK (VERIFICATO da DEX/manifest): AppLovin MAX con mediazione (Meta Audience Network, Unity Ads e altri), Google Play Billing (`com.android.vending.BILLING`), Facebook SDK.

## Classi e identificatori (VERIFICATO: i nomi esistono come MonoScript in `globalgamemanagers.assets`)
BenefitShopSlot_*, Shop*Table, VipTable, ReqCostInfoTable, PostFixedTable, AdsManager, CurrencyDto, CurrencyEndpoint, Quest*Table, Attendance*Table, RewardTable. Elenco: `CLASSES/by_system/ECONOMY_SHOP.csv` (euristico, contiene falsi positivi).

## Relazioni con altri sistemi
Tutti: costi e ricompense attraversano ReqCostInfoTable/RewardTable.

## Pseudocodice (RICOSTRUITO, formule NON note)
```
buy(p): se limite raggiunto: no; paga (gratis | valuta | store); accredita reward; conta acquisto
```

## Codice C# originale
`RECONSTRUCTED_CODE/Economy/Economy.cs` — PurchaseService (con IStoreGateway), QuestProgress.

## Affidabilità
Categorie: alta. Prezzi, pacchetti, limiti: nessuna.

## Non recuperabile
Nessun valore numerico (statistiche, costi, probabilità, formule) è stato recuperato: le righe delle tabelle non sono presenti nei file Unity non protetti dell'APK, e i campi/metodi delle classi non sono leggibili perché il metadata IL2CPP è trasformato (non toccato). Prezzi e pacchetti reali dipendono dal server/store.

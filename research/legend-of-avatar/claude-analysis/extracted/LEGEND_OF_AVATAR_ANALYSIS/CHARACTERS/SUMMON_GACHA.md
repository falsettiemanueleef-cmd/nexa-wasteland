# Evocazione (gacha)

Etichette: **VERIFICATO** (letto dal file), **RICOSTRUITO** (dedotto da evidenze), **IPOTESI**, **NON RECUPERABILE**.

## Funzionamento
- VERIFICATO: esistono tre viste di evocazione: `GachaViewPolymorph`, `GachaViewSkill`, `GachaViewSpirit`.
- RICOSTRUITO: le evocazioni hanno livello (`SummonLevelUpTable`, `GachaLevelBar`), pool (`SummonPoolTable`), costi (`ShopSummonCostTable`), informazioni sui tassi (`GachaRateInfo`) e tabelle di selezione (`PickTable`). Anche Emblem e Blessing hanno evocazioni con bonus (`EmblemSummonBonusTable`, `EmblemSummonBonusPointTable`, `BlessingSummonRateRow`).
- IPOTESI: `SummonStressTable` / `GachaStressInfoPopUp` descrivono un contatore tipo pity; non provato.

## Classi e identificatori (VERIFICATO: i nomi esistono come MonoScript in `globalgamemanagers.assets`)
GachaUI, GachaResultUI, GachaRewardCard, GachaRateInfo, GachaLevelBar, GachaLevelAndButtons, GachaStressInfoPopUp, SummonInfo/Pool/LevelUp/StressTable, ShopSummonTable, ShopSummonCostTable, PickTable, SummonForRelicTable. Elenco: `CLASSES/by_system/SUMMON_GACHA.csv`.

## Relazioni con altri sistemi
Personaggi, Equipaggiamento, Economia (costi in valuta), Dungeon (esiste `DungeonGachaStageTable`).

## Pseudocodice (RICOSTRUITO, formule NON note)
```
roll(pool): i = weightedPick(pool.weights)   // pesi sconosciuti
(IPOTESI) se pityRule.shouldForce(counter): pick elemento del grado forzato
```

## Codice C# originale
`RECONSTRUCTED_CODE/Summon/Summon.cs` — SummonService con IPityRule opzionale (di default NoPity).

## Affidabilità
Categorie e tabelle: alta. Pity: bassa (ipotesi).

## Non recuperabile
Nessun valore numerico (statistiche, costi, probabilità, formule) è stato recuperato: le righe delle tabelle non sono presenti nei file Unity non protetti dell'APK, e i campi/metodi delle classi non sono leggibili perché il metadata IL2CPP è trasformato (non toccato). Tassi, pool, costi e presenza reale del pity sono NON RECUPERABILI.

# Equipaggiamento e potenziamenti

Etichette: **VERIFICATO** (letto dal file), **RICOSTRUITO** (dedotto da evidenze), **IPOTESI**, **NON RECUPERABILE**.

## Funzionamento
- RICOSTRUITO: più sottosistemi paralleli: ItemGear (template, upgrade, conversione, reset opzioni, successione, condizioni d'uso), UpgradableGear (con rune e sinergie), Emblem (info, enhance, sinergie, bonus di evocazione), Essentials (info, enhance, sinergie), Blessing (info, sintesi, evocazione), Vessel (gradi, livelli), Guardian (Star/Stone), Halidom (skin) e Relic/Rune.
- RICOSTRUITO: opzioni statistiche casuali da pool (`StatOptionPoolTable`, `StatOptionPassiveTable`, `StatOptionEnhanceBonusTable`) e bonus di set (`SetBonusTable`).
- VERIFICATO: classi `ItemGearServerData`/`ItemServerData`: lo stato degli oggetti ha una controparte server.

## Classi e identificatori (VERIFICATO: i nomi esistono come MonoScript in `globalgamemanagers.assets`)
ItemGear*Table (10), UpgradableGear*Table (5), Emblem*Table (6), Essentials*Table (4), BlessingInfoTable, VesselGradeTable, VesselLevelUpTable, GuardianStar/StoneTable, HalidomSkinTable, StatOption*Table (5), SetBonusTable, SynthesisTable, CraftInfoTable, AlchemyLabTable. Elenco: `CLASSES/by_system/EQUIPMENT.csv` (273 classi, raggruppate per prefisso, quindi con qualche falso positivo).

## Relazioni con altri sistemi
Evocazione (Emblem/Blessing summon), Dungeon (ogni tipo di equipaggiamento ha un dungeon di farm: Accessory, Emblem, Relic, Rune, Vessel, Halidom), Economia (costi).

## Pseudocodice (RICOSTRUITO, formule NON note)
```
enhance(gear): paga costo(gear); if random < rate(gear): level++ else (forse) level--   // tassi/penalità sconosciuti
synergy(set, count): unisci i bonus di tutti i tier con requiredCount <= count
```

## Codice C# originale
`RECONSTRUCTED_CODE/Equipment/Equipment.cs` — EnhanceService, StatOptionPool, SynergyEvaluator.

## Affidabilità
Sottosistemi esistenti: alta. Regole: bassa.

## Non recuperabile
Nessun valore numerico (statistiche, costi, probabilità, formule) è stato recuperato: le righe delle tabelle non sono presenti nei file Unity non protetti dell'APK, e i campi/metodi delle classi non sono leggibili perché il metadata IL2CPP è trasformato (non toccato). Statistiche, bonus, costi e tassi di successo sono NON RECUPERABILI.

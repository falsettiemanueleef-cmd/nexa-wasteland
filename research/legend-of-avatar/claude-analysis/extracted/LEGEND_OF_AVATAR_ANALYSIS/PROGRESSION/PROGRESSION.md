# Progressione, stage, dungeon, risorse

Etichette: **VERIFICATO** (letto dal file), **RICOSTRUITO** (dedotto da evidenze), **IPOTESI**, **NON RECUPERABILE**.

## Funzionamento
- RICOSTRUITO: avventura per capitoli/stage (`ChapterTable`, `StageTable`, `AdventureGameMode`) con penalità (`StagePenaltyTable`) e un test A/B sugli stage (`StageABTestGroupTable`, `StageABTestMobTable`); slot dei goblin del tesoro (`StageGoblinSlotTable`).
- RICOSTRUITO: ricompense automatiche/offline (`AutoBattleRewardGroup`); IPOTESI che la pubblicità ne raddoppi/aumenti (`AutoBattleRewardADPopup`).
- VERIFICATO: dungeon di risorse: Accessory, Emblem, Event, Gacha, Halidom, Relic, Rune, Upgrade, Vessel; e torri: Basic, Combo, Counter, Skill.
- RICOSTRUITO: crescita (`GrowthLevelUp`, `GrowthTrain`, `GrowthHardTrain`, `GrowthAmplify(+Rate)`, `GrowthSpecialize`, `GrowthUpgrade`), Pantheon (`PantheonOfferingRewardTable`), Cargo (navigazione/pirateria), Garden (tombe, semi, raccolti), Collezione, Presenze (`Attendance*`), Tutorial.

## Classi e identificatori (VERIFICATO: i nomi esistono come MonoScript in `globalgamemanagers.assets`)
ChapterTable, StageTable, Dungeon*StageTable (11), DungeonTower*Table (4), Growth*Table (9), CargoInfo/Schedule/Slot/UpgradeTable, GardenTomb*Table, Collection*Table, Attendance*Table, TutorialTable. Elenco: `CLASSES/by_system/PROGRESSION.csv`.

## Relazioni con altri sistemi
Combattimento (stage), Equipaggiamento (i dungeon danno materiali), Economia (costi di crescita), Quest.

## Pseudocodice (RICOSTRUITO, formule NON note)
```
claim(last, now): accrued = min(now - last, MAX_ACCRUAL); reward = rateFor(stage) * accrued   // MAX e rate sconosciuti
enterDungeon(): if usedToday >= limit: deny   // limiti sconosciuti
```

## Codice C# originale
`RECONSTRUCTED_CODE/Progression/Progression.cs` — OfflineRewards, DailyEntryTracker, LevelCurve (soglie iniettate).

## Affidabilità
Moduli di gioco: alta. Numeri e formule: nessuna.

## Non recuperabile
Nessun valore numerico (statistiche, costi, probabilità, formule) è stato recuperato: le righe delle tabelle non sono presenti nei file Unity non protetti dell'APK, e i campi/metodi delle classi non sono leggibili perché il metadata IL2CPP è trasformato (non toccato). Curve di esperienza, ricompense per stage, limiti giornalieri e parametri dei boss sono NON RECUPERABILI.

# Sistema di combattimento

Etichette: **VERIFICATO** (letto dal file), **RICOSTRUITO** (dedotto da evidenze), **IPOTESI**, **NON RECUPERABILE**.

## Funzionamento
- RICOSTRUITO: il combattimento è una simulazione (`BattleSimulator`) alimentata da una formazione (`BattleFormation`), che produce eventi (`BattleEvent`) registrati in un `BattleRecord`; esiste un `BattleReplaySimulator` che rigioca gli eventi (`ReplayUnit`, `ReplayPetUnit`, `ReplayRaidBossUnit`, `ReplayTriggerSkillUnit`).
- RICOSTRUITO: le abilità sono composte da effetti tipizzati: Attack, Barrier, Buff, Extra, FixedDamage, Heal, Installed, Reflect, Shield (una classe `SkillEffect*Table` per ciascuno), con condizioni (`SkillConditionTable`) e sequenze d'azione (`ActionSequenceTable`, `MonsterActionSequenceData`).
- IPOTESI: `BattleModTimeDmgTable` e `BattleModTimeDmgLogicData` indicano un modificatore di danno legato al tempo (forse anti-stallo). `TimeAccelerationTable` indica velocità di gioco selezionabili.
- RICOSTRUITO: ci sono classi di verifica/audit (`BattleIntegrityAuthentication`, `BattleVerificationRuntimeHost`, `BattleVerificationTransport`, `BattleTamperDiagnostics`, `ReplayEventAudit`): l'esito delle battaglie sembra essere verificabile dal server. Non è stato analizzato come funzionano e non verrà ricostruito.

## Classi e identificatori (VERIFICATO: i nomi esistono come MonoScript in `globalgamemanagers.assets`)
BattleSimulator (8 varianti MonoScript), BattleSimulatorManager, BattleFormation, BattleEvent, BattleRecord, BattleTime, BattleResultBase/Simple, BattleMap, BattleSpawnPoint, MonsterTable, MonsterUnit, MonsterLogicData, BossInfo, BossHpGauge, BossChallengeGameMode/InfoTable/StageTable, BotTable, SkillTable, SkillConditionTable, SkillEffect{Attack,Barrier,Buff,Extra,FixedDamage,Heal,Installed,Reflect,Shield}Table, PlayerBuffTable, AdBuffTable, SpawnTable, SpawnPartyTable, StageTable, StagePenaltyTable, DamageDisplayImage. Elenco completo: `CLASSES/by_system/BATTLE.csv`.

## Relazioni con altri sistemi
Personaggi (statistiche delle unità), Progressione (StageTable/ChapterTable), Guild/PvP (replay e raid riusano i `Replay*Unit`), Economia (`AdBuffTable`: buff legati alle pubblicità).

## Pseudocodice (RICOSTRUITO, formule NON note)
```
tick = 0
for each unit: unit.next = initialDelay(unit)   // sconosciuto
loop until team eliminated or maxTicks:
  for each living unit with next <= tick:
     target = selectTarget(unit)          // regola sconosciuta
     damage, crit = computeDamage(...)    // formula sconosciuta
     target.hp -= damage; log event
     unit.next = tick + interval(unit)    // dipende dalla velocità, formula sconosciuta
```

## Codice C# originale
`RECONSTRUCTED_CODE/Battle/Battle.cs` — loop deterministico con RNG seminato; tutte le regole sono iniettate tramite `IBattleRules` e l'implementazione predefinita `UnrecoveredRules` lancia `NotRecoveredException`.

## Affidabilità
Struttura (nomi di classi e tipi di effetto): alta. Funzionamento interno: bassa. Il codice è un'architettura generica, non il gioco originale.

## Non recuperabile
Nessun valore numerico (statistiche, costi, probabilità, formule) è stato recuperato: le righe delle tabelle non sono presenti nei file Unity non protetti dell'APK, e i campi/metodi delle classi non sono leggibili perché il metadata IL2CPP è trasformato (non toccato). Anche formule di danno, critici, velocità e difficoltà sono NON RECUPERABILI.

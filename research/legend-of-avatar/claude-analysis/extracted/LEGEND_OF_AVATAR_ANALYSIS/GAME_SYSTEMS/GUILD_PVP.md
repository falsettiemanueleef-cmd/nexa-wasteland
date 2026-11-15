# Gilde, PvP, classifiche, chat

Etichette: **VERIFICATO** (letto dal file), **RICOSTRUITO** (dedotto da evidenze), **IPOTESI**, **NON RECUPERABILE**.

## Funzionamento
- RICOSTRUITO: Guild con livelli (`GuildLevelUpTable`), statua (`GuildStatueTable`), stemmi, piazza (`GuildPlazaManager`); Guild Boss (`GuildBossGameMode`, `GuildBossRewardTable`); Guild Raid con stagioni, programmazione, contributi, abilità di gruppo (`GuildRaid*Table`); Guild War 5vs5 (`GuildWar5vs5GameMode`) con modalità replay Buff War e Leader War e un torneo (`GuildWarTournamantGameMode`); Arena (`ArenaGameMode`, `ArenaStandardTable`); classifiche (`RankingInfoTable`, `RankingRewardTable`); chat con traduzione, blocchi e segnalazioni (`ChatManager`).
- RICOSTRUITO: queste modalità appaiono basate su replay/simulazioni (`GuildWarLeaderSimDataHelper`).

## Classi e identificatori (VERIFICATO: i nomi esistono come MonoScript in `globalgamemanagers.assets`)
GuildBossGameMode, GuildRaid* (≈25), GuildWar* (≈40), ArenaGameMode, ChatManager e relative classi. Elenco: `CLASSES/by_system/GUILD_PVP.csv`.

## Relazioni con altri sistemi
Combattimento (`Replay*Unit`), Economia (ricompense), Classifiche.

## Pseudocodice (RICOSTRUITO, formule NON note)
```
seasonActive(now) = start <= now < end
rewardFor(rank) = band che contiene rank
```

## Codice C# originale
`RECONSTRUCTED_CODE/Guild/GuildPvp.cs` — SeasonWindow, RankRewardTable, GuildState.

## Affidabilità
Elenco modalità: alta. Regole, matchmaking, punteggi: nessuna.

## Non recuperabile
Nessun valore numerico (statistiche, costi, probabilità, formule) è stato recuperato: le righe delle tabelle non sono presenti nei file Unity non protetti dell'APK, e i campi/metodi delle classi non sono leggibili perché il metadata IL2CPP è trasformato (non toccato). Il matchmaking e le classifiche vivono sul server.

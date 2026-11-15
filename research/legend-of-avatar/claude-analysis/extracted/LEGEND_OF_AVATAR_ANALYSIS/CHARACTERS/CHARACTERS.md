# Personaggi, Polymorph, Spirit, Pet

Etichette: **VERIFICATO** (letto dal file), **RICOSTRUITO** (dedotto da evidenze), **IPOTESI**, **NON RECUPERABILE**.

## Funzionamento
- RICOSTRUITO: il «personaggio» giocabile è modellato da `PolymorphTable` (trasformazioni/forme), con `PolymorphBackTable` (oggetto da schiena), `SpiritTable` (spiriti con `SpiritLeaderEffect`), pet (`PetActor`, `PetUnit`, sprite `Icon_Pet_*`) e `CharacterTable`.
- RICOSTRUITO: esistono sintesi (`PolymorphSynthesisRateInfo`), potenziamenti (`PolymorphEnhancePerkTable`, `PolymorphSpiritEnhanceTable`), punteggio (`PolymorphScoreTable`), albero di specializzazione (`GrowthSpecializeTreeTable`, `SpecializeTreeViewLock`) e trascendenza a stelle con ricompense di 6 tipi (DecoTitle, PolymorphBack, PolymorphGear, ProfileFrame, ProfileMotionPortrait, TranscendenceSkill).
- VERIFICATO: nei nomi degli sprite compaiono `icon_polymorphGear_WP_M1_Katana_008`, `icon_polymorphGear_WP_S1_Chakram_008`: l'arma è un'entità di Polymorph e i nomi seguono schema tipo/serie/numero.

## Classi e identificatori (VERIFICATO: i nomi esistono come MonoScript in `globalgamemanagers.assets`)
Polymorph* (≈45 classi), Spirit* , Pet*, CharacterTable, VisionForgeTable, GrowthSpecializeTable/TreeTable. Elenco: `CLASSES/by_system/CHARACTER_POLYMORPH.csv`.

## Relazioni con altri sistemi
Evocazione (GachaViewPolymorph/Spirit), Equipaggiamento (PolymorphGear), Progressione (Growth*), Collezione (CollectionItemPolymorph/Spirit), Decorazione (Deco*).

## Pseudocodice (RICOSTRUITO, formule NON note)
```
synthesize(inputs): ok = random < rate(inputs); result = template(inputs, ok)   // tassi e input sconosciuti
unlock(node): richiede tutti i prerequisiti sbloccati + costo
```

## Codice C# originale
`RECONSTRUCTED_CODE/Characters/Characters.cs` — SynthesisService (regole iniettate) e SpecializeTree (logica di sblocco con prerequisiti).

## Affidabilità
Elenco funzionalità: media-alta (da nomi di classi e sprite). Logica: bassa.

## Non recuperabile
Nessun valore numerico (statistiche, costi, probabilità, formule) è stato recuperato: le righe delle tabelle non sono presenti nei file Unity non protetti dell'APK, e i campi/metodi delle classi non sono leggibili perché il metadata IL2CPP è trasformato (non toccato). Rarità, classi, trasformazioni, evoluzioni e probabilità di sintesi sono NON RECUPERABILI.

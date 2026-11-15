# Elementi non recuperati

| Elemento | Motivo |
|---|---|
| Codice sorgente C# originale | Non presente: il codice nativo/IL2CPP non è nei percorsi standard e i metadati sono trasformati. Non è stato fatto alcun tentativo di decodifica. |
| Campi, proprietà, metodi, firme, pseudocodice reale | Richiedono il metadata IL2CPP (protetto). I MonoScript danno solo nomi. |
| Righe delle 203 tabelle (personaggi, skill, stage, costi, tassi, pity, ricompense) | Non presenti in file Unity non protetti; potrebbero essere in `assets/e8Hk2vi4CH` (non analizzato), in contenuti scaricati o sul server. |
| Formule di danno, critici, velocità, difficoltà | Dipendono dal codice e dalle tabelle. |
| Valori dei MonoBehaviour dei sistemi di gioco | Senza type tree né metadata non si possono interpretare i campi; in `level0` ci sono solo componenti UI. |
| Pixel di sprite/texture | Inventariati ma non decodificati (ASTC non decodificabile senza libreria; nessuna rete). |
| Modelli 3D, prefab dei sistemi, scene di gioco | Non presenti: l'unica scena è `level0`; il resto è probabilmente in bundle non inclusi. |
| Audio, localizzazione (stringhe) | Nessun AudioClip né tabella di stringhe nei file estratti. |
| Parametri server (matchmaking, classifiche, valute, acquisti) | Server-side. |
| `libData.so`, `ATG_E*.sec`, `e8Hk2vi4CH` | Componenti con protezione/trasformazione: fuori dall'ambito richiesto. |

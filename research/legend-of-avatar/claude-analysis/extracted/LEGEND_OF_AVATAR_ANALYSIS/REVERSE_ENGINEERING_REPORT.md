# Rapporto tecnico — Legend of Avatar APK (analisi statica non invasiva)

Ambito: solo dati non protetti. Nessuna decifratura, nessuna estrazione di chiavi, nessun aggiramento di protezioni o anti-cheat. Il `global-metadata.dat` (intestazione `ED 56 6B FF`) e il blob `assets/e8Hk2vi4CH` NON sono stati decodificati né analizzati oltre l'intestazione.

## 1. Cosa è stato verificato (VERIFICATO)
- APK: 869 voci (`DUMP/apk_entries.csv`). Manifest: permessi INTERNET, BILLING, VIBRATE, WAKE_LOCK, DUMP. DEX: SDK pubblicitari/rete (AppLovin, Meta Audience Network, Unity Ads, OkHttp); nessun codice di gioco.
- `data.unity3d`: UnityFS v8, Unity 6000.3.27f2, 1.086 blocchi LZ4HC, 142.336.624 byte decompressi, 7 file virtuali (identici ai risultati preliminari). Decompresso con parser scritto da zero (`TOOLS/unityfs.py`).
- File serializzati v22 **senza type tree**: `globalgamemanagers`, `globalgamemanagers.assets`, `sharedassets0.assets`, `level0`, `unity_builtin_extra` (`DUMP/serialized_files_summary.json`).
- **6.033 MonoScript** con classe/namespace/assembly in chiaro (91 assembly). Assembly-CSharp: 3.114 voci (2.958 nomi unici), di cui 1.296 in `P1`, 211 in `P1.UINew`, 61 in `P1Core`, 44 in `P1Core.UI`; 203 classi `*Table`. Terze parti riconosciute dai namespace: Behavior Designer, Odin, DOTween, TextMeshPro, Unity Localization, Unity Input System, URP, Facebook SDK, Unity InferenceEngine.
- Scena `level0` = schermata iniziale/caricamento UI (GameObject `StartScene`, `LoadingState`, `TopPopUpRoot`...): 315 GameObject, 414 MonoBehaviour.
- Asset: 2.007 Sprite (nomi), 96 Texture2D (49 Alpha8, 29 ASTC 4x4, 8 ASTC 6x6, 2 ASTC 8x8, 6 RGBA32, 2 RGB24), 82 Material/Shader/Font/Animation, 465 MonoBehaviour (`ASSETS/`).

## 2. Cosa NON c'è nei dati accessibili (NON RECUPERABILE)
- Nessun `TextAsset`, nessun ScriptableObject di dati, nessuna riga delle 203 tabelle: i dati di gioco non sono nei file Unity non protetti.
- Nessun campo, metodo, firma o corpo di metodo: i MonoScript contengono solo nome classe/namespace/assembly. Per i campi serve il metadata IL2CPP, che è trasformato.
- Nessuna formula, statistica, probabilità o costo.

## 3. Ricostruzione
Per ogni sistema vedi i file `.md` nelle cartelle `BATTLE_SYSTEM`, `CHARACTERS`, `EQUIPMENT`, `PROGRESSION`, `ECONOMY`, `GAME_SYSTEMS`. Il raggruppamento delle classi per sistema (`CLASSES/by_system/`) è euristico, per prefisso del nome (RICOSTRUITO): contiene falsi positivi (es. task di Behavior Designer come `AddForce` in ECONOMY_SHOP) e 1.532 classi in OTHER.

Osservazioni architetturali (RICOSTRUITO): stato di valute/oggetti con controparte server (`CurrencyDto`, `ItemServerData`, `ItemGearServerData`); battaglie registrate come eventi con classi di verifica/audit; gioco data-driven con ≈203 tabelle; contenuti scaricati/risolti tramite Addressables (`AddressableAutoRelease`) — IPOTESI per la parte Addressables.

## 4. Codice
`RECONSTRUCTED_CODE/` contiene 8 file C# (502 righe) scritti da zero. **Non è codice recuperato**; **non è stato compilato** (nessun compilatore C# nell'ambiente; controllato solo il bilanciamento di parentesi). Nessun valore di gioco è stato inventato: le regole sono interfacce iniettate e il default lancia `NotRecoveredException`.

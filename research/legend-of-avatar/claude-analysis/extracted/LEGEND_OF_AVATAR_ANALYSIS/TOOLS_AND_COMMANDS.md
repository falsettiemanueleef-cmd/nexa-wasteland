# Strumenti e procedure realmente usati

- Ambiente: Linux, Python 3.12, `unzip`, `xxd`; senza rete (impossibile installare UnityPy, Il2CppDumper, AssetRipper, Ghidra: **non usati**).
- Nessun compilatore C# disponibile.

Procedura (script in `TOOLS/`):
1. `unzip Legend_of_Avatar.apk`
2. `python3 unityfs.py assets/bin/Data/data.unity3d vfs/` — parser UnityFS v8 + decompressore LZ4 in Python puro (i blocchi dati iniziano dopo l'allineamento a 16 byte della block-info).
3. `sf.py` — parser SerializedFile v22 senza type tree (header esteso, tipi, oggetti, script types, externals).
4. `extract1.py` — MonoScript (layout fisso: m_Name, ExecutionOrder, Hash128, ClassName, Namespace, AssemblyName).
5. `extract2.py` — MonoBehaviour/GameObject/Sprite/oggetti vari. `extract3.py` — Texture2D (layout Unity 6: nessun campo ForcedFallbackFormat; StreamData letto dal path resS).
6. `systems.py` — raggruppamento euristico delle classi per prefisso.
Il manifest è stato letto cercando stringhe UTF-16 (non decodificato con un vero parser AXML).

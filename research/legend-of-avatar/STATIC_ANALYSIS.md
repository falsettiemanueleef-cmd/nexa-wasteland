# Legend of Avatar APK — static analysis record

> Status: preliminary static triage. This is an analysis of the supplied APK, not recovered original source code.

## Scope and handling
- Sample: `Legend of Avatar.apk` (about 103.4 MB).
- Analysis performed on the APK archive and extracted files without installing or running the game.
- The APK and its proprietary assets/source are **not** committed to this repository.
- Purpose: understand publicly observable technical structure and inform an original NEXA WASTELAND implementation. Do not redistribute the game's code, assets, or proprietary data.

## Verified observations
- APK archive contains 869 ZIP entries.
- Unity data is present at `assets/bin/Data/data.unity3d` (61,577,906 bytes).
- Metadata is present at `assets/bin/Data/Managed/Metadata/global-metadata.dat` (21,150,852 bytes).
- Metadata begins with bytes `ED 56 6B FF`, not the standard IL2CPP metadata magic visible in clear. This suggests a transformed/nonstandard format, but does not by itself prove how it is protected.
- Four architecture-specific ELF payloads exist under `assets/`: `ATG_E.sec`, `ATG_E_x64.sec`, `ATG_E_x86.sec`, `ATG_E_x86_64.sec`.
- The native library symbol/string inventory includes names associated with asset access and cryptographic routines, including `GetAssetsFile`, `GetAssetsFileInfo`, `decrypt_byteArray`, `decrypt_part_byte_arry`, `aes_set_key`, `aes_decrypt`, `aes_encrypt`, and `encryptDecrypt`. Names alone do not prove the exact runtime behavior.
- No standard `lib/<abi>/libil2cpp.so` path was found in the APK inventory.
- The Unity bundle has a `UnityFS` signature and reports Unity version `6000.3.27f2`.
- DEX inspection so far primarily surfaced Android/platform and advertising/analytics SDK references; no recovered game C# source has been established.

## Additional APK inventory finding (2026-11-15)
- The APK also contains `assets/libData.so` (2,179 bytes). Despite its extension, it is not an ELF shared object; it begins with bytes `50 4B 11 30`. Its format and purpose are not established.
- Rechecked package paths: no ordinary `libil2cpp.so`, `GameAssembly.dll`, or `Assembly-CSharp.dll` was found in the inspected APK paths. The four `ATG_E*.sec` files are ELF shared objects and expose asset/decryption/AES-related functions.
- Research found that the standard Perfare Il2CppDumper documents Unity support through 2022.2 and explicitly says deobfuscating protected metadata is outside its scope. AssetRipper's current documentation lists Unity support through 6000.4.x. A third-party Il2CppDumper fork claims APK ingestion and handling of some enveloped/simple-obfuscation formats; this is a tool claim, not a verified result on this sample.

## Not yet recovered
- No original C# source files have been extracted.
- No verified class/method map for the gameplay systems exists yet.
- Unity asset bundle contents have not yet been decoded into usable assets.
- The metadata format and the role of the opaque payloads remain unresolved.
- No server-side rules, drop rates, gacha odds, combat formulas, or progression formulas have been verified from code.

## Environment limitations encountered
- The analysis container had no working DNS/internet access during this pass.
- UnityPy, JADX, Apktool, Cpp2IL, AssetRipper, and Il2CppDumper were not installed in the environment.
- Consequently, no claim is made that a decompiler has successfully processed this APK.

## Next technical work
1. Use compatible Unity tooling to inspect the UnityFS bundle and enumerate extractable objects.
2. Test the metadata with current IL2CPP analysis tools and record exact tool versions and output.
3. Identify the native loading path through static symbol and disassembly analysis; document evidence, not assumptions.
4. Reassess whether class/method recovery is technically possible from the supplied package without bypassing access controls.
5. Translate observed gameplay behavior into original NEXA systems; do not copy proprietary source or assets.

## Evidence discipline
Each future finding must be tagged as one of:
- **Verified** — directly observed in a file/tool output.
- **Inferred** — plausible interpretation with evidence and caveats.
- **Unknown** — not established.

Update this file when new tests are completed. Preserve previous conclusions when correcting them; explain the correction in the project decision log.

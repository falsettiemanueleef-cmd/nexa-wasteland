# Legend of Avatar — research toolchain

Research date: 2026-11-15

## Candidate tools

| Tool | Official source | Relevant capability | Important limitation |
|---|---|---|---|
| Il2CppDumper (Perfare) | https://github.com/Perfare/Il2CppDumper | Restores metadata-derived dummy DLLs and generates Ghidra/IDA structures for supported IL2CPP binaries. | Its README explicitly says obfuscated metadata deobfuscation is out of scope; it supports Unity 5.3–2022.2 in the documented version. |
| Cpp2IL | https://github.com/SamboyCoding/Cpp2IL | IL2CPP analysis/decompilation framework. | Requires compatible binary/metadata inputs; no success on this sample has been demonstrated. |
| AssetRipper | https://github.com/AssetRipper/AssetRipper | Parses Unity assets and bundles and converts extractable content into Unity project assets. | Asset support quality varies by Unity version; it does not recover original gameplay source code. Current docs list Unity 3.5 through 6000.4.x. |
| Il2CppDumper fork (MyDearMoon) | https://github.com/MyDearMoon/il2cpp-Dumper | Claims APK/XAPK ingestion, automatic extraction of IL2CPP binary/metadata, and handling of some enveloped headers and simple obfuscation. | Third-party fork; claims require validation. Its documentation describes encrypted metadata as requiring runtime extraction in some cases. |
| Ghidra | https://ghidra-sre.org/ | Static disassembly and analysis of native ELF libraries. | Produces analysis/disassembly, not original C# source. |

## Findings specific to this APK

- The package contains `global-metadata.dat` but its first four bytes are `ED 56 6B FF`, not the usual IL2CPP metadata magic. This is a nonstandard/transformed header; it does not identify the transform by itself.
- Four architecture variants named `ATG_E*.sec` are ELF shared objects. Their exported symbols include asset access, decryption and AES routines (`GetAssetsFile`, `GetAssetsFileInfo`, `decrypt_byteArray`, `decrypt_part_byte_arry`, `aes_set_key`, `aes_decrypt`, `aes_encrypt`, `encryptDecrypt`).
- The APK also contains `assets/libData.so` (2,179 bytes). It is **not an ELF shared object**: it begins with bytes `50 4B 11 30`, so the filename is misleading. Its actual format/purpose is not yet established.
- No ordinary `libil2cpp.so`, `GameAssembly.dll`, or `Assembly-CSharp.dll` was found in the APK paths inspected.
- The Unity data bundle reports Unity `6000.3.27f2`; AssetRipper's current documentation covers Unity versions through `6000.4.x`, making it the best first asset-inspection candidate.
- The static toolchain has not yet been run against the sample because this analysis runtime has no network access/package installation and does not currently contain the required Windows/.NET tool binaries.

## Safe execution order

1. Inventory all APK paths and hashes; preserve the original sample unchanged.
2. Test a static parser against the APK and metadata. Record exact version, command, exit code and output.
3. If metadata parsing fails, stop and classify the format as unresolved. Do not assume a generic XOR/envelope fix.
4. Run AssetRipper against the UnityFS bundle to enumerate assets; record what succeeds and what fails.
5. Use Ghidra or equivalent to inspect the public symbol surface and call relationships of the supplied native loader.
6. Do not use process injection, root-only dumpers, anti-cheat bypasses, credential extraction, server intrusion or DRM circumvention. If runtime-only access is required, seek an authorized debug build or permission from the publisher.
7. Reconstruct original NEXA mechanics from independently documented observations; do not copy proprietary code or game assets.

## Evidence labels

- **Verified:** directly observed in this APK or a tool's actual output.
- **Tool claim:** stated by a project's own documentation, not yet tested here.
- **Inferred:** supported interpretation, but not proven.
- **Unknown:** no reliable evidence yet.

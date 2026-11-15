# Legend of Avatar — public web research

Research date: 2026-11-15

## Public identification
- Apple App Store lists **Legend of Avatar** with developer/seller **Billionairegames** and describes it as an idle RPG with automatic/offline progression, hero transformation, resource/domain development, guilds, dungeons and PvP.
  Source: https://apps.apple.com/it/app/legend-of-avatar/id6746192853
- APKPure identifies the Android package as `com.billionairegames.serares`. Its version listing reports version 1.27.1 dated 2026-09-23. This is a third-party catalogue, not an official developer source.
  Source: https://apkpure.net/legend-of-avatar/com.billionairegames.serares/download
- Searches for the exact APK-specific native identifiers `ATG_E_x64.sec`, `GetAssetsFile`, `BattleVerificationTransport`, and `P1Core.Network.NetworkManager` did not surface a public source-code repository or a public code dump. This is a bounded search result, not proof that no such material exists anywhere.

## Tool documentation reviewed
- Perfare Il2CppDumper: https://github.com/Perfare/Il2CppDumper
  Its README says it can restore dummy DLLs (not method code), supports ELF/ELF64 and several other binary formats, and generates Ghidra/IDA scripts. It explicitly says deobfuscating protected metadata is outside its scope.
- AssetRipper: https://github.com/AssetRipper/AssetRipper
  Its documentation says it analyzes Unity game files and converts extractable assets into Unity-native formats. Current docs list Unity 3.5.0 through 6000.4.x, which includes the Unity 6000.3.27f2 version string observed in this APK.
- MyDearMoon Il2CppDumper fork: https://github.com/MyDearMoon/il2cpp-Dumper
  Its README claims APK/XAPK ingestion and automatic extraction of some enveloped or lightly obfuscated metadata formats. These are upstream claims; the tool has not been run on this sample. The README also describes encrypted metadata cases where runtime extraction is needed. No such runtime method is being attempted here.
- Longno242 IL2CPP-Dumper: https://github.com/Longno242/IL2CPP-Dumper
  Its README distinguishes static disk analysis from an in-process runtime dumper. The latter is not part of this static workflow.

## What the web research does and does not establish
- No publicly indexed original source code was found in the searches performed.
- Public store/catalogue pages identify the app and provide a high-level feature description; they do not disclose source code, server logic, balance tables or internal gameplay formulas.
- Tool documentation identifies candidate software only. A tool is not considered successful until it has been executed on this exact APK and its output verified.

## Next actions
1. Try a static APK-aware metadata parser against the supplied APK using a controlled local environment.
2. If it cannot parse the nonstandard metadata, retain the failure output and investigate only documented, authorized formats; do not perform runtime injection or defeat access controls.
3. Enumerate Unity assets with AssetRipper.
4. Continue indexing the clear-text sidecar metadata and native symbol names.
5. Keep NEXA WASTELAND's implementation original; use findings to understand general game architecture and mechanics, not to copy proprietary source/assets.

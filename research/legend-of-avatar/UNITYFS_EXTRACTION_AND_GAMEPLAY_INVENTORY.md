# UnityFS extraction and gameplay identifier inventory

Date: 2026-11-15  
Target: `Legend of Avatar.apk` already supplied in this workspace  
Scope: static inspection of the APK's embedded UnityFS bundle. No game execution, runtime memory access, account access, or server interaction.

## 1. Confirmed bundle structure

The APK contains `assets/bin/Data/data.unity3d`, a UnityFS bundle:

- UnityFS format version: **8**
- Unity player version field: `5.x.x`
- Engine version: **6000.3.27f2**
- Declared bundle size: **61,577,906 bytes**
- Block-info compressed/uncompressed sizes: **4,476 / 11,184 bytes**
- Header flags: **0x243**
- Block-info starts at byte offset **64** (16-byte alignment)
- Block table: **1,086 data blocks**
- Block-info compression: LZ4HC (flag value 3)
- Data blocks: LZ4 (flag value 2)
- All 1,086 blocks were decompressed successfully using the system LZ4 library.
- Total reconstructed virtual content: **142,336,624 bytes**.

The UnityFS directory contains seven virtual files:

| Virtual file | Offset | Size | Flag |
|---|---:|---:|---:|
| globalgamemanagers | 0 | 505,700 | 0x4 |
| Resources/unity_builtin_extra | 505,700 | 721,908 | 0x4 |
| globalgamemanagers.assets | 1,227,608 | 2,152,336 | 0x4 |
| sharedassets0.assets | 3,379,944 | 44,510,992 | 0x4 |
| globalgamemanagers.assets.resS | 47,890,936 | 4,169,408 | 0 |
| sharedassets0.assets.resS | 52,060,344 | 89,856,000 | 0 |
| level0 | 141,916,344 | 420,280 | 0x4 |

The reconstructed data was written locally as `loa_bundle_content.bin`; a directory index was written as `loa_bundle_index.txt`. The extraction is a real completed parse of the UnityFS container and LZ4 blocks, not a claim that all Unity serialized objects have been decoded.

## 2. What the extracted strings reveal

A printable-string scan of the reconstructed content produced approximately **2.11 million lines**. Many are engine, shader, UI, localization, and asset strings. Among them are numerous coherent game-system identifiers. These names are direct evidence that the corresponding identifiers/assets are present in the bundle; they do not, by themselves, prove that every feature is enabled in the current live game or explain its exact rules.

### Progression and combat identifiers

Examples found:

- `AdventureGameMode`, `AdventureGameUI`, `AdventureSelectSlot`
- `AutoBattleRewardADPopup`, `AutoBattleRewardGroup`, `AutoBattleRewardInfoPopup`
- `ChapterTable`
- `CurrentInfo_Stage_BreakThrough`, `CurrentInfo_Stage_Loop`
- `BattleFormation`, `BattleMap`, `BattleRecord`, `BattleReplaySimulator`
- `BattleSimulator`, `BattleSimulator.Hit`, `BattleSimulator.Unit`
- `BattleResultUI`, `BattleStatisticsPop`
- `BossChallengeGameMode`, `BossChallengeStageTable`
- `DungeonGameMode`, `DungeonBattleUI`, `DungeonInfoTable`

### Character development, equipment and collectible systems

Examples found:

- `CharacterTable`, `CharacterUI`, `CharacterElement`
- `GachaUI`, `GachaResultUI`, `GachaRewardCard`, `GachaRateInfo`
- `GachaViewPolymorph`, `GachaViewSkill`, `GachaViewSpirit`
- `CollectionDataTable`, `CollectionInfoTable`
- `CollectionItemPolymorph`, `CollectionItemPolymorphGear`, `CollectionItemSkill`, `CollectionItemSpirit`
- `AccessoryPage`, `AccessoryPopup`
- `EmblemEnhanceTable`, `EmblemSynthesis`-related UI identifiers, and `EmblemInfoTable`
- `BlessingInfoTable`, `BlessingSynthesisPage`, `BlessingRefinementView`
- `DungeonAccessoryStageTable`, `DungeonEmblemStageTable`, `DungeonRelicStageTable`, `DungeonRuneStageTable`, `DungeonVesselStageTable`
- `HUDSlot_Relic`, `HUDSlot_Rune`, `HUDSlot_Wing`

This is substantially broader than a simple three-style character system: the asset inventory contains multiple parallel collection, equipment, enhancement, and summon-related interfaces. Their exact role and progression dependencies still need object/config decoding.

### Economy, rewards and monetization identifiers

Examples found:

- `BenefitShopSlot_Free`, `BenefitShopSlot_IAP`, `BenefitShopSlot_IAP_RepeatBuy`
- `BenefitShopSlot_Monthly`, `BenefitShopSlot_Monthly_Cargo`, `BenefitShopSlot_Monthly_Mine`, `BenefitShopSlot_Monthly_Train`
- `BenefitShopSlot_RemoveAd`
- `BattlePassBuyPopup`, `BattlePassInfoPopup`, `BattlePassRewardPopup`
- `AutoBattleRewardADPopup`
- `CargoUpgradeTable`, `CargoScheduleTable`, `CargoRewardGroup`
- `GrowthLevelUpTable`, `GrowthTrainTable`, `GrowthAmplifyTable`, `GrowthSpecializeTreeTable`
- `AdvanceTrainLevelUpSequencer`, `AdvanceTrainRewardCard`

These identifiers confirm that the client bundle includes free/IAP/monthly/remove-ad shop presentation, battle-pass UI, ad-reward UI, cargo progression, and several growth/training subsystems. They do not establish prices, purchase odds, or server-side economy rules.

### Social, competitive and guild systems

Examples found:

- `GuildUI`, `GuildCreatePop`, `GuildManagementUI`, `GuildMemberSlot`
- `GuildBossGameMode`, `GuildBossRewardTable`
- `GuildRaidMainUI`, `GuildRaidBattleUI`, `GuildRaidScheduleTable`
- `GuildWarMainUI`, `GuildWar5vs5GameMode`, `GuildWarTournamentBattleUI`
- `ArenaGameMode`, `ArenaStandardTable`, `ArenaUI`
- `ChatManager`, `ChatUI`, `ChatTranslationPacket`
- `BattleReplaySimulator`, `GuildWarReplayPopup`

### Quests, events and recurring engagement

Examples found:

- `DailyQuestView`, `DailyQuestScore`
- `AchievementQuestUI`, `AchievementProgress`
- `ClientQuestData`, `ClientEventQuestScoreData`
- `AttendancePopUp`, `AttendanceRewardTable`
- `EventAttendancePopup`, `EventBossChallengeTable`
- `Anniversary_1st_MainEvent_Part1_Popup`, `Anniversary_1st_MainEvent_Part2_Popup`
- `AlchemyLabQuestInfoPopup`, `AlchemyLabTable`

## 3. Important security / integrity indicators

The same extracted bundle contains identifiers such as:

- `BattleIntegrityAuthentication`
- `BattleTamperDiagnostics`, `BattleTamperReporting`
- `BattleVerificationDiagnostics`, `BattleVerificationRuntimeHost`, `BattleVerificationTransport`
- `BossCheatDiagnostics`
- `CodeStage.AntiCheat.*`

This indicates that the client includes battle-integrity/verification and anti-cheat-related components. It does not establish their server protocol or implementation details. No attempt was made to disable, evade, or bypass them.

## 4. Implications for NEXA WASTELAND

The strongest design-relevant finding is that the reference game has a much larger progression/economy surface than its visible idle combat loop suggests. The inventory points to several distinct systems:

1. Core stage/adventure progression and automated battle rewards.
2. Character/polymorph, skill and spirit collection.
3. Multiple equipment/collection tracks: accessory, emblem, blessing, relic, rune, vessel and wing.
4. Several growth tracks, including training, amplification and specialization.
5. Separate dungeon challenge tracks for different equipment categories.
6. Guild boss, guild raid, guild war, arena and replay/record systems.
7. Daily quests, achievements, attendance, seasonal events and battle pass.
8. Free, ad-reward, monthly and IAP shop surfaces.

For NEXA, this is useful as a map of market conventions and progression complexity—not a blueprint to copy. We should select a smaller number of original systems, use original names/art/rules, and avoid reproducing the reference game's UI, assets, text, or distinctive implementation.

## 5. What is not recovered yet

- No C# method bodies or full source code.
- No verified decoded game database/config tables.
- No verified drop rates, summon probabilities, item stats, upgrade costs, or monetization values.
- No proof that each named system is currently active or accessible in the live game.
- No server-side logic or backend data.

The next technical milestone is to parse the Unity serialized files (`globalgamemanagers`, `globalgamemanagers.assets`, `sharedassets0.assets`, `level0`) and extract object names, MonoBehaviour type references, TextAssets, and ScriptableObject/config content. This requires a Unity serialized-file parser compatible with Unity 6000.3.27f2. AssetRipper's published support range includes Unity 6000.4.x, although its documentation warns that support quality can vary by Unity version: https://assetripper.github.io/AssetRipper/

## 6. Reproducibility notes

The bundle header and directory values above were read directly from the supplied APK. Block-info and data blocks were decompressed using LZ4. The 2.11-million-line string inventory was generated from the reconstructed virtual content using printable ASCII string extraction. Identifier examples above are taken from that inventory.

UnityFS format/flag interpretation reference: https://reverseengineering.wiki/index.php/Format%3AUnity_asset_bundle_%28UnityFS%29  
Official Unity compression overview: https://docs.unity.com/en-us/engine/6000.5/manual/assets-and-media/assets-managing-runtime/assetbundles-section/creating/compression-format

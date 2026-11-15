# Legend of Avatar — runtime initialization metadata index

Source files in supplied APK:
- `assets/bin/Data/ScriptingAssemblies.json`
- `assets/bin/Data/RuntimeInitializeOnLoads.json`

These are Unity-generated metadata sidecars. They expose assembly names and selected runtime-initialization method identifiers, not method bodies or complete source code.

## Assembly inventory
`ScriptingAssemblies.json` lists 200 assembly names. Relevant examples:
- `Assembly-CSharp.dll`
- `Assembly-CSharp-firstpass.dll`
- `P1.BossCheatDiagnostics.Core.dll`
- `P1.ReconnectAutoRecovery.Core.dll`
- `P1.ResourceAssets.Runtime.dll`
- `BillionaireGames.NativeIap.dll`
- `BehaviorDesigner.Runtime.dll`
- `ACTk.Runtime.dll`

## Assembly-CSharp runtime initialization entries
There are 20 entries for `Assembly-CSharp` among 68 total runtime initialization records:

| Namespace | Class | Method |
|---|---|---|
| (global) | GraphicsBootReporter | _LogGraphicsApiAtBoot |
| P1Core | String | _Initialize |
| P1Core | ConfigSafeArea | _ApplyOnBoot |
| P1Core | Utils | _InitializeNumericalFormatter |
| P1Core | Utils | _InitializeString |
| P1Core.Network | NetworkManager | _Initialize |
| P1Core.Network | NetworkManager | _ResetPrestoProcessScopedFlags |
| P1 | SoundList | _Initialize |
| P1 | AdViewProcessLog | _ResetStaticState |
| P1 | AdsManager | _ResetStaticSdkState |
| P1 | BattleTamperReporting | _ResetStaticState |
| P1 | BattleVerificationTransport | _ResetStaticState |
| P1 | BattleVerificationDiagnostics | _ResetStaticState |
| P1 | BattleVerificationRuntimeHost | _ResetStaticState |
| P1 | InAppPurchaseToastMessageUI | _Initialize |
| P1 | ToastMessageUI | _Initialize |
| P1 | BottomMenu | _Initialize |
| P1 | BuffRewardToastMessage | _Initialize |
| P1 | VirtualCursorManager | _ResetStatics |
| P1 | VirtualCursorManager | _Initialize |

## Interpretation limits
- These records prove that Unity's runtime startup metadata references the listed identifiers.
- They do not establish what the methods do internally.
- The names related to network and battle verification justify prioritizing those areas for further *static* metadata analysis, but no server protocol or anti-cheat implementation has been reconstructed.

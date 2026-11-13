# NEXA GAME STUDIO — Production Architecture

**Document status:** Approved working baseline  
**Version:** 1.0  
**Date:** 2026-11-13  
**Budget baseline:** EUR 0  
**Primary development device:** Android smartphone (Realme GT7T 5G)  
**Current reference project:** NEXA: WASTELAND

---

## 1. Mission and operating principles

NEXA GAME STUDIO is the production workflow used to design, build, test and prepare original games for release with the greatest practical automation available.

Operating rules:

1. Spend no money unless the owner explicitly approves it.
2. Prefer free and open-source tools and assets with clear commercial-use rights.
3. Preserve existing projects, history and decisions; do not restart without a documented reason.
4. Separate verified facts, assumptions, proposals and unfinished work.
5. Never report a feature, build, test, export or publication as complete unless evidence confirms it.
6. Keep the owner's manual work limited to creative direction, important approvals and real-device testing.
7. Use incremental milestones. A prototype is not a finished game.
8. Check licenses, platform requirements, service quotas and costs before adoption or release.

## 2. Standard toolchain

| Area | Standard | Role | Status / limits |
|---|---|---|---|
| Main engine | Godot 4.7.2, GDScript, Compatibility renderer | Game runtime, scenes, logic, UI, physics and animation | Adopted for NEXA: WASTELAND; Android editor has practical limitations |
| Source of truth | GitHub repository | Source files, version history, issue tracking and project documents | Connected GitHub actions can read and write supported repository files |
| Automated validation | GitHub Actions | Headless import, parse checks, automated tests and later build jobs | Free quota applies; successful headless tests do not prove visual correctness |
| Android device | Godot Android Editor | Open, run and inspect project on device | Real-device validation remains required |
| Android build support | GABE (Godot Android Build Environment) | Android export/build support from Android workflow | Must be installed and tested on the actual device before treating as production-ready |
| Image creation | ChatGPT ImageGen | Concept art, character concepts, backgrounds, props and visual exploration | Generated images still need slicing, cleanup, animation and in-game validation |
| Pixel-art editing | Dixel or Pixel Studio | Sprite cleanup, frame editing and sprite-sheet preparation | Free-tier features and export limits must be checked in the installed version |
| Free art assets | Kenney (CC0 collections) | Placeholder and production-ready assets where suitable | Verify the license/readme for the exact downloaded pack |
| Other asset library | OpenGameArt | Optional art, music and sound | Check each asset's individual license and attribution requirements |
| Sound generation | jfxr / jsfxr | Original game sound effects | Check the specific tool's license and exported audio rights |
| Recorded sound | Freesound | Optional ambience and effects | Prefer CC0; CC-BY requires attribution; exclude non-commercial assets from commercial builds |
| Terminal | Termux | Optional Git, scripts and file operations on Android | Not a desktop replacement; not required for routine GitHub edits |
| Code editor | Acode | Optional local text editing | Not required for routine GitHub edits |
| Alternative engine | GDevelop | Evaluate only if a concrete workflow advantage appears | Not selected; cloud features and free-tier quotas may be limited |
| Alternative engine | Defold | Evaluate only if a concrete workflow advantage appears | Not selected; official editor workflow is desktop-oriented |

### Engine decision

Godot remains the default engine. The current project already uses Godot and has passed headless validation in GitHub Actions. Replacing it with GDevelop or Defold would create migration work without a demonstrated benefit.

A future engine change requires a written comparison showing a material improvement in Android workflow, technical capability, cost or delivery risk, plus owner approval.

## 3. System architecture

The standard production loop is:

1. **Owner brief:** the owner describes the desired player experience and approves major creative decisions.
2. **Design:** ChatGPT prepares the game design, technical plan, asset list and acceptance criteria.
3. **Implementation:** ChatGPT uses connected GitHub operations to create or update project files when supported.
4. **Automated validation:** GitHub Actions imports and runs the project headlessly; additional tests are added as systems mature.
5. **Device validation:** the owner opens the updated project/build on Android and reports visual, touch, performance or device-specific issues.
6. **Correction:** implementation is revised against the reported issue and acceptance criteria.
7. **Release gate:** builds, assets, licenses, store requirements and owner approval are checked before release.

GitHub is the source of truth for project files and documentation. The Android device is the source of truth for real-device behavior. ChatGPT is the design and implementation assistant, not a claim of a permanently connected remote desktop.

## 4. Automation boundaries

### Can be automated or performed through connected tools

- Read and update supported text files in the GitHub repository.
- Generate scripts, configuration, documentation and structured project data.
- Create commits and preserve change history where GitHub write tools permit.
- Run repository workflows and inspect workflow results/logs where connector permissions permit.
- Perform headless import and runtime checks in CI.
- Prepare build workflows after export prerequisites are verified.
- Generate concept images using available image-generation tools.

### Requires explicit evidence or owner/device action

- Visual quality, animation timing, touch ergonomics and actual Android performance.
- Device-specific crashes, rendering defects, battery use and memory behavior.
- Installing or granting permissions to Android applications.
- Account ownership, signing credentials, store identity and legal declarations.
- Paid purchases, subscriptions, store fees and publication approval.
- Any operation for which the relevant integration is not actually connected.

A tool or MCP server is not considered integrated merely because its repository or documentation exists. Integration status changes only after a real connection and successful test.

## 5. Project structure convention

Use this structure as projects mature; do not create empty folders merely to imitate it.

```text
project/
  project.godot
  README.md
  docs/
    GDD.md
    PRODUCTION_ARCHITECTURE.md
    TECHNICAL_DESIGN.md
    QA_PLAN.md
    ASSET_REGISTER.md
    RELEASE_CHECKLIST.md
  scenes/
  scripts/
  assets/
    characters/
    enemies/
    environments/
    ui/
    audio/
    effects/
  data/
  tests/
  .github/
    workflows/
```

Existing paths and project conventions take precedence. Restructure only when there is a concrete maintenance benefit and the change can be validated.

## 6. Asset and license policy

Every external asset used in a distributable build must have a record in `docs/ASSET_REGISTER.md` containing:

- Asset name and exact source URL.
- Author/creator, if supplied.
- License name and license URL or included license file.
- Required attribution text.
- Whether commercial use and modification are allowed.
- Local project path and date acquired.

Default preferences:

1. Original assets created for the project.
2. Kenney CC0 assets after checking the pack.
3. OpenGameArt or Freesound assets only after individual license review.
4. Do not use assets marked non-commercial in a commercial game.
5. Do not assume that a website being free means every asset on it is free for commercial use.

Keep third-party license files and attribution notices in the repository where required.

## 7. Quality gates

A milestone is not complete until its acceptance criteria have evidence.

| Gate | Minimum evidence |
|---|---|
| Design approved | Written design and owner approval |
| Implementation complete | Relevant files committed and feature behavior described |
| Static/headless validation | CI run succeeds; logs reviewed |
| Playable milestone | Game launched and core interactions tested on Android |
| Performance milestone | Target device test with recorded observations |
| Android package | Export preset, templates/build environment and successful APK/AAB artifact |
| Release candidate | Regression checklist, license register, store assets and install test |
| Publication | Store submission accepted/published; public listing verified |

Headless success proves only that the configured automated checks passed. It does not prove visual correctness, complete gameplay, mobile usability or release readiness.

## 8. Android build policy

For Godot Android export:

- Confirm the Android export preset and its exact name.
- Confirm matching Godot export templates.
- Confirm Android SDK, JDK and Gradle requirements for the selected Godot version and build route.
- Keep signing keys and passwords out of public repositories and logs.
- Use a debug APK for internal device testing.
- Use a properly signed release artifact for store distribution.
- Do not claim a build exists until the artifact is produced and can be downloaded/installed.
- If GABE is used, validate the complete export path on the owner's Android device before relying on it.

## 9. Cost and service policy

The target is EUR 0 until the owner approves otherwise.

- Prefer free tiers and local/open-source tools.
- Check current free-tier quotas before adding CI-heavy workflows.
- GitHub Actions usage may be subject to included minutes, storage and account limits.
- Image-generation availability may be limited by the ChatGPT plan.
- Google Play, Steam, hosting, backend services, paid assets, legal/accounting advice and professional services may introduce costs.
- Present unavoidable costs before the owner commits to them.
- Never imply that a free development workflow makes commercial distribution or long-term operation cost-free.

## 10. Change control and owner approvals

The owner is responsible for:
- Core concept and desired player experience.
- Approving major design, monetization and scope decisions.
- Installing/authorizing required Android tools.
- Testing builds on the physical device.
- Store accounts, legal declarations, signing identity and publication approval.
- Approving any spending.

ChatGPT is responsible for:
- Research and technical comparisons.
- Drafting and maintaining design/technical documents.
- Creating and modifying supported project files.
- Preparing test plans and automated checks.
- Reporting risks, uncertainty, costs and verified results.
- Avoiding unnecessary manual work for the owner.

No spending, account creation, public release, irreversible deletion or material monetization change is assumed approved by a general request to proceed with planning.

## 11. Current status: NEXA: WASTELAND

The existing NEXA: WASTELAND project remains the reference project. This document establishes its production workflow; it does not start a new game or claim that the current prototype is a finished product.

Previously recorded evidence:
- Godot 4.7.2 project exists in the GitHub repository.
- GitHub Actions headless import and runtime validation succeeded for the recorded runs.
- Those checks do not validate graphics, touch controls or performance on the Realme device.
- Android export preset and a verified APK have not yet been established in the project status recorded for this document.

Repository: https://github.com/falsettiemanueleef-cmd/nexa-wasteland

## 12. Next operational sequence

1. Preserve the current project and its history.
2. Review the existing GDD against the implemented prototype.
3. Confirm the current Android editor and GABE workflow on the actual device.
4. Establish a reproducible debug APK export route.
5. Add a concise QA checklist for the existing vertical slice.
6. Only then resume feature development against the approved MVP scope.

---

**Status vocabulary used in all NEXA GAME STUDIO reports**

- **Verified:** directly tested or confirmed by a reliable result.
- **Implemented:** code/files exist, but behavior may still require testing.
- **Proposed:** recommendation not yet adopted.
- **Blocked:** requires an unavailable integration, owner action, credential or external dependency.
- **Not verified:** insufficient evidence to claim success.

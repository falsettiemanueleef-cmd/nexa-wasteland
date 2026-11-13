# NEXA: WASTELAND — Game Design Document

**Version:** 0.3  
**Status:** Updated design baseline — side-scrolling Action RPG  
**Platform:** Android first  
**Engine:** Godot 4 / GDScript / Compatibility renderer

## 1. High concept

After **The Collapse**, the player reactivates the abandoned NEXA-01 bunker. Its onboard AI contains fragmented records of the old world and guides the survivor through a vast hostile wasteland. The player explores side-scrolling environments, fights enemies, defeats bosses, collects resources and equipment, and develops a chosen hero.

NEXA: WASTELAND combines:
- Side-scrolling 2D action combat.
- Incremental RPG progression and equipment growth.
- Automatic basic attacks combined with manual combos and active abilities.
- A bunker hub and resource-management layer.
- A long-term campaign target of 100 distinct worlds.
- Character-class advancement through milestone quests.
- An optional premium roster of five Divine-class heroes.

## 2. Core player fantasy

Become a wasteland survivor whose fighting style visibly evolves. The weapon in the character's hands changes the moveset; equipped armor and gear change the character's appearance; class abilities create distinct combat identities. The journey begins as an Adventurer and advances through milestone quests. Every expedition should provide a reason to improve the hero and push into the next level.

## 3. Campaign structure

### Full-game target
- **100 worlds**, each with a distinct environment, visual identity, enemy roster, hazards and atmosphere.
- **10 levels per world**, for a long-term target of **1,000 levels**.
- A boss encounter at **level 5** and another at **level 10** of every world.
- Defeating the level-10 boss unlocks the next world.
- The level-5 boss acts as a mid-world progression gate and reward milestone.
- Each world introduces variations in enemy behavior, environmental threats, encounter composition and rewards.

### Tower of Challenges
The game also includes a long-term Tower of Challenges running in parallel with the main campaign.
- The tower is structured to mirror campaign progression: **one tower floor for each campaign stage**. With 100 worlds and 10 stages per world, the initial full-game target is **1,000 floors**.
- Each floor is a self-contained challenge with enemies and rewards. Completing floors grants rewards whose value and potential rarity increase as the player climbs.
- The tower is designed to be extendable beyond floor 1,000, allowing effectively endless progression without changing the campaign's initial 1,000-stage structure.
- Access to the first tower floor requires a minimum character level or promotion rank; the exact entry requirement remains to be defined.
- Promotion rank gates access to successive 100-floor sections: a higher promotion rank is required to enter each next block of 100 floors. The exact rank names and unlock requirements remain to be defined.
- The tower is expandable through future content updates. A planned expansion can add **100 further worlds**, each with 10 stages, and therefore **1,000 additional tower floors**; later expansions can continue extending both structures.
- Expansion updates may also introduce new promotion ranks and additional Divine-class heroes. The number, identities and exact release cadence of those heroes remain to be designed.
- The tower is a parallel progression mode, not a replacement for the campaign. Its difficulty and reward curve must be balanced independently while remaining relevant to campaign and character progression.

The 100-world structure is the content vision, not the first production milestone. Worlds will be produced in themed batches using reusable environment and encounter systems while preserving a distinct identity for each world.

### World themes
Potential themes include ruined outskirts, overgrown suburbs, collapsed megacities, industrial wastelands, contaminated hospitals, radioactive deserts, frozen territories, flooded districts, abandoned military complexes, underground laboratories and NEXA research zones. These are examples for the world-building pipeline, not the final list of all 100 worlds.

## 4. Characters and classes

### Base roster: five classes
The full base game has **five distinct classes**. Each class can be selected in a masculine or feminine presentation. The two presentations must preserve the class identity while providing distinct character models/silhouettes and appropriate animations.

Each base class has:
- A recognizable silhouette, outfit language and animation identity.
- Class-specific base statistics and combat role.
- Exclusive active abilities.
- A class skill progression tree.
- Defined weapon compatibility.
- Equipment slots and visible gear changes.

The initial class archetypes carried forward from the original design are:
1. Fighter — durable frontline combatant.
2. Scout — mobile explorer and evasive fighter.
3. Medic — support-oriented survivor with recovery and sustain tools.
4. Technician — tactical specialist using devices and battlefield utilities.
5. Sniper — precision-focused ranged specialist.

Exact names, visual designs, statistics, ability lists and weapon compatibility matrices remain to be designed and balanced. Weapon compatibility must make thematic and mechanical sense; not every class needs to equip every weapon family.

### Class progression and advancement quests
- Every base-class character begins at the initial rank **Adventurer**.
- Class advancement is tied to milestone quests in the campaign.
- A class-advancement quest becomes available at every **10-world milestone**: after worlds 10, 20, 30, and so on.
- The intended campaign cadence is one advancement quest per ten worlds. The exact unlock timing, quest objectives, rewards, subsequent rank names and number of ranks remain to be defined.
- Completing an advancement quest promotes the character along their class path and can unlock new class features, skills or visual distinctions.
- The rank sequence beyond Adventurer is deliberately undecided and will be designed later.

### Premium roster: five Divine-class heroes
In addition to the five base classes, the game plans **five additional heroes of Divine class**.
- These are distinct, individually designed playable heroes inspired by mythological deities and legendary or historically famous warriors. Inspiration may be drawn from multiple mythologies and warrior traditions; the characters must still have original names, designs, lore and interpretations rather than being direct copies.
- Each Divine hero has a fixed identity and presentation. **There is no masculine/feminine selection** for Divine heroes: each is a singular divine character.
- Divine heroes have **higher base statistics** than the five Adventurer-origin base classes. Their superior starting attributes are an intentional premium-roster distinction.
- Each Divine hero has a highly distinctive, visually spectacular appearance, signature effects and a **unique exclusive moveset** that sets the character apart from every base class and the other Divine heroes.
- Each Divine hero has their own signature weapon, designed as an integral part of that hero's identity and moveset. The weapon's visual design is fixed: it **cannot be replaced or visually transformed into another weapon**.
- A Divine weapon can still be upgraded through rarity tiers. Rarity progression may improve its power and add controlled refinement to effects or materials, but it must preserve the weapon's recognizable original design and silhouette.
- The player's **hero level** and **available unspent skill points** are account-wide/shared progression values: they carry over when switching between heroes, including when unlocking a Divine hero.
- Every playable character has their **own character-specific skill tree**. Shared available skill points can be allocated in the currently selected character's unique tree; unlocked nodes and allocated points remain associated with that character and do not automatically unlock the same skills on other heroes.
- Core currencies and progression resources are shared across the roster, including **coins** and **diamonds**. Diamonds are obtainable in **small quantities through gameplay/farming**; additional diamonds are intended to be purchasable. The exact earn rates, prices and purchase limits remain to be balanced.
- Equipment progression is shared: the player's equipment inventory and **equipment rarity progression** are retained when changing heroes. Character-specific signature weapons remain bound to their Divine hero; their rarity can increase while their defining appearance remains fixed.
- Switching heroes therefore changes the playable character, its unique skill tree and combat identity, but does not reset the shared hero level, unspent skill-point pool, currencies or equipment rarity progress.
- These five heroes are intended to be unlocked through paid access. Prices, purchase method and platform-store integration remain to be defined.
- Their higher base power is deliberate. Overall encounter and progression tuning must account for the Divine roster while keeping the five base classes viable and enjoyable; Divine heroes are premium, stronger alternatives, not cosmetic-only variants.

### Account character slots and independent runs
- Each account has one main character slot and **five additional empty character slots** available for creating or trying other characters.
- The main character's campaign progress and save remain attached to the main slot. Creating or starting a character in another slot must not reset, overwrite or transfer the main character's progress.
- A character created in an empty slot starts a **new progression run from zero**, with its own campaign progress. Each slot maintains its own character/run save.
- This is different from switching between heroes within an already established roster/run: the previously defined shared hero progression and roster-switching rules apply within that run. Starting a fresh character in another slot is an independent beginning.
- The exact handling of account-level currencies, purchases and any shared entitlements across separate slots remains to be specified.

### Character progression
Character systems may include level, experience, health, attack, defense, speed, critical chance, abilities, equipment and class-specific attributes. Skill upgrades should create meaningful choices rather than only increasing numbers.

## 5. Weapons and equipment

### Initial weapon families
- Swords.
- Scythes.
- Dual maces.
- Dual blades.

Each weapon family defines its own:
- Attack speed, reach, impact and recovery.
- Basic attack animation.
- Combo chain and finisher.
- Hitbox timing and movement behavior.
- Compatible active skills or weapon modifiers.
- Visual effects and impact feedback.

### Combo design
- Weapon movesets use **3-hit or 5-hit** combo chains.
- The weapon equipped determines the combo length and animation sequence.
- Combo inputs are manual and timing-sensitive, while basic attacks can continue automatically when the player is not issuing a combo input.
- Combo finishers should feel distinct through animation, hit reaction, sound and visual impact.
- The system must support interruption, recovery windows and enemy hit reactions.

Initial design examples:
- Heavy weapon styles can use shorter, high-impact 3-hit chains.
- Fast dual weapons can use longer 5-hit chains.
- Swords and scythes may use either chain length depending on the specific weapon variant.

These are starting design principles; final movesets require playtesting.

### Visible equipment adaptation
Equipped items must visibly affect the character model, not only inventory statistics. Armor, helmets, shoulder pieces, gloves, boots and weapons should appear on the character where supported by the art rig. Equipment tiers and rarity can introduce increasingly distinctive silhouettes, materials and effects.

The system should be modular so gear combinations do not require a completely new character animation set for every item.

## 6. Combat system

### Hybrid combat model
- Basic attacks can resolve automatically when an enemy is in range and the character is able to attack.
- The player can manually input weapon combos.
- The player activates class abilities and special skills through touch controls.
- Movement and positioning remain player-controlled.
- Defensive actions, dodges, interrupts or other tactical actions may be added according to class and weapon design.

### Cinematic presentation
Combat should feel impactful and readable rather than becoming an uncontrolled sequence of effects. Use:
- Weapon-specific attack animations and combo transitions.
- Distinct hit reactions and enemy stagger states.
- Strong but brief impact effects.
- Clear telegraphs for dangerous enemy and boss attacks.
- Camera emphasis for selected finishers and boss moments, without taking control away for too long.
- Readable damage, health and ability feedback on a mobile screen.

### Boss encounters
Bosses appear at levels 5 and 10 in every world. Each boss should have a recognizable silhouette, attack patterns, telegraphs, phases or escalating behavior, and rewards. Boss difficulty should test preparation, equipment and player execution rather than rely only on inflated health.

## 7. Core gameplay loop

1. Return to or manage the NEXA-01 bunker.
2. Upgrade the character, skills and equipment.
3. Select a world and level.
4. Enter a side-scrolling combat stage.
5. Explore, move, fight and use combos/abilities.
6. Defeat enemies and collect resources or loot.
7. Defeat the level boss when reaching level 5 or 10.
8. Complete class-advancement quests at ten-world milestones when available.
9. Return with rewards, improve the survivor and unlock further content.

## 8. Bunker and resources

Planned bunker rooms:
- Command Center.
- Storage.
- Workshop.
- Infirmary.
- Generator.

Core resources:
- Scrap.
- Food.
- Water.
- Fuel.
- Medicine.

The bunker is the meta-progression hub. It supports preparation, crafting or upgrading, storage and recovery. The first playable milestone needs only a minimal hub or equipment/reward screen; the full room-management layer comes later.

## 9. Incremental and offline progression

The game may provide limited passive resource generation and expedition returns while the player is away. Offline progression must be capped and balanced so active play remains worthwhile.

- Initial prototype: no offline simulation required.
- First MVP target: local offline progress, with a proposed cap of four hours.
- Passive progress must not automatically trivialize bosses or replace active combat.

## 10. World and content production

The 100-world target requires reusable systems:
- Modular tilesets and parallax backgrounds.
- Reusable enemy behavior components with world-specific variants.
- Data-driven level definitions, spawn groups and rewards.
- Reusable boss state machines with unique attack patterns.
- Weapon data and animation configuration separated from character logic.
- Localization-ready text and UI.

The goal is to build content through reusable systems without making worlds visually or mechanically interchangeable.

## 11. Technical direction

- Godot 4 and GDScript.
- Compatibility renderer for broad Android support.
- 2D side-scrolling scene structure.
- Touch-friendly movement, combo and ability controls.
- Local device save.
- Data-driven resources for classes, weapons, equipment, enemies, levels and rewards.
- Planned languages: Italian, English, Spanish, French and German.

Performance targets and final screen resolution must be validated on the target Android device. The project must be tested in Godot Android Editor before claiming device compatibility.

## 12. Monetization and online features

Potential future options:
- Paid unlocks for the five additional Divine-class heroes.
- Optional rewarded ads.
- Cosmetic items.
- Narrative or world expansions.

The five Divine heroes are a planned premium character offering with intentionally higher base statistics, fixed divine identities, spectacular visual designs, unique movesets and signature weapons. Hero level, unspent skill points, currencies and equipment rarity progression are shared across the roster, while each character retains its own skill tree and allocated build. Diamonds can be earned in small quantities through gameplay and additional amounts are intended to be purchasable. Prices, purchase flow, platform-store integration and detailed progression balance remain undecided. The base classes must remain viable, while the premium heroes are clearly stronger alternatives.

Online accounts, cloud saves, leaderboards, events, multiplayer, chat, clans, PvP, stores, payments, ads and complex backend systems are outside the first playable milestone. Paid hero unlocks are a future full-game feature, not part of the first prototype.

## 13. First playable milestone (vertical slice)

The first milestone is deliberately much smaller than the full-game vision:
- One side-scrolling environment.
- One playable base class, beginning as Adventurer, with masculine and feminine presentation if the art pipeline allows it.
- One weapon family with a complete 3-hit or 5-hit moveset.
- Automatic basic attacks.
- Manual combo input.
- One active class ability.
- One standard enemy.
- One boss encounter.
- Basic health, damage, rewards and equipment feedback.
- Touch controls and local save.

The next milestone can add a second weapon family, additional enemies, visible equipment swaps and a second class. Expand only after the first slice is playable and performs reliably on Android.

## 14. MVP acceptance criteria

- Project opens and runs in Godot 4.
- Player can move through a side-scrolling test environment.
- Basic attacks work automatically under defined conditions.
- Manual combo input triggers the equipped weapon's moveset.
- One active ability can be triggered by the player.
- Enemy damage, defeat and reward flow work.
- A boss encounter can be completed or failed.
- At least one equipment change is reflected in gameplay or appearance, when the art slice supports it.
- Progress can be saved locally.
- Controls and performance are usable on the target Android device.

## 15. Development rule

The 100-world campaign, class-advancement quest chain and five Divine heroes are long-term ambitions. Do not attempt to build all campaign content or every character before validating the core combat. First deliver one polished vertical slice; then expand the combat framework, character roster, gear system, class progression and world pipeline in measurable stages.

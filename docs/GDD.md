# NEXA: WASTELAND — Game Design Document

**Version:** 0.2  
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

## 2. Core player fantasy

Become a wasteland survivor whose fighting style visibly evolves. The weapon in the character's hands changes the moveset; equipped armor and gear change the character's appearance; class abilities create distinct combat identities. Every expedition should provide a reason to improve the hero and push into the next level.

## 3. Campaign structure

### Full-game target
- **100 worlds**, each with a distinct environment, visual identity, enemy roster, hazards and atmosphere.
- **10 levels per world**, for a long-term target of **1,000 levels**.
- A boss encounter at **level 5** and another at **level 10** of every world.
- Defeating the level-10 boss unlocks the next world.
- The level-5 boss acts as a mid-world progression gate and reward milestone.
- Each world introduces variations in enemy behavior, environmental threats, encounter composition and rewards.

The 100-world structure is the content vision, not the first production milestone. Worlds will be produced in themed batches using reusable environment and encounter systems while preserving a distinct identity for each world.

### World themes
Potential themes include ruined outskirts, overgrown suburbs, collapsed megacities, industrial wastelands, contaminated hospitals, radioactive deserts, frozen territories, flooded districts, abandoned military complexes, underground laboratories and NEXA research zones. These are examples for the world-building pipeline, not the final list of all 100 worlds.

## 4. Characters and classes

The full game has **five distinct classes**. Each class can be selected in a masculine or feminine presentation. The two presentations must preserve the class identity while providing distinct character models/silhouettes and appropriate animations.

Each class has:
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
8. Return with rewards, improve the survivor and unlock further content.

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
- Optional rewarded ads.
- Cosmetic items.
- Narrative or world expansions.

No pay-to-win design. Online accounts, cloud saves, leaderboards, events, multiplayer, chat, clans, PvP, stores, payments, ads and complex backend systems are outside the first playable milestone.

## 13. First playable milestone (vertical slice)

The first milestone is deliberately much smaller than the full-game vision:
- One side-scrolling environment.
- One playable class, with masculine and feminine presentation if the art pipeline allows it.
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

The 100-world campaign is the long-term ambition. Do not attempt to build 1,000 levels or all five complete classes before validating the core combat. First deliver one polished vertical slice; then expand the combat framework, character roster, gear system and world pipeline in measurable stages.

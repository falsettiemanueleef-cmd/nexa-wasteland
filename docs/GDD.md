# NEXA: WASTELAND — Game Design Document

**Version:** 0.1  
**Status:** Initial design baseline

## High concept
After **The Collapse**, the player reactivates the abandoned NEXA-01 bunker. Its onboard AI contains fragmented records of the old world and guides the player through survival, expeditions, and discoveries.

## Core loop
1. Collect and manage resources.
2. Upgrade the bunker.
3. Prepare a character for an expedition.
4. Select a zone and deploy.
5. Resolve combat and tactical decisions.
6. Collect rewards and recover.
7. Unlock new zones and story fragments.

## Resources
Scrap, food, water, fuel, and medicine.

## Bunker
Planned rooms: Command Center, Storage, Workshop, Infirmary, Generator.  
**MVP:** Command Center and Storage.

## Characters
Stats: level, experience, health, attack, defense, speed, abilities, equipment.  
Planned classes: Fighter, Scout, Medic, Technician, Sniper.  
**MVP:** one playable character.

## Combat
Basic attacks resolve automatically. The player can intervene with abilities, healing, buffs, or retreat decisions.  
**MVP:** one player, one enemy, automatic attacks, one active skill, health bars, victory/defeat.

## World zones
Outskirts, Ruined City, Industrial Area, Contaminated Hospital, Radioactive Desert, Military Base, Faction Territory, NEXA Research Center.  
**MVP:** first zone only; later zones can appear as locked map slots.

## Offline progression
Offline progression is capped at four hours in the MVP. The game must remain playable without internet access.

## Technical direction
- Godot 4, GDScript, Compatibility renderer
- Android first; touch-friendly interface
- Local device save
- Planned languages: Italian, English, Spanish, French, German

## Monetization and online features
Potential future options: optional rewarded ads, cosmetics, narrative expansions. No pay-to-win. Online accounts, cloud saves, leaderboards, events, multiplayer, chat, clans, PvP, stores, payments, ads, and complex backend are outside the MVP.

## MVP acceptance criteria
- Project opens and runs in Godot 4.
- Player can view the bunker hub.
- Player can start a basic expedition.
- One enemy can be defeated or defeat the player.
- Victory grants a reward.
- Progress can be saved locally.

## Development rule
Build a small playable RPG loop first. Add depth only after it works reliably on the target Android device.

# NEXA: WASTELAND

**Android-first 2D side-scrolling Action RPG** with incremental progression, automatic basic attacks, player-triggered combos and active abilities. Built with Godot 4 and GDScript.

## Project status
Early development — design updated to GDD v0.2. The repository currently contains a bootstrap screen, not a playable combat prototype.

## Vision
After **The Collapse**, the player reactivates the abandoned NEXA-01 bunker and ventures into a hostile wasteland. Explore distinct worlds, fight enemies and bosses, collect resources and equipment, and evolve a survivor into a powerful fighter.

## Core design
- Side-scrolling 2D action RPG; Android first
- Five distinct classes, each selectable in masculine or feminine presentation
- Hybrid combat: automatic basic attacks, manual weapon combos and active class abilities
- Weapon-specific movesets and 3-hit or 5-hit combo chains
- Initial weapon families: swords, scythes, dual maces and dual blades
- Equipment visibly changes the character's appearance
- Long-term campaign target: 100 distinct worlds, 10 levels each (1,000 levels); bosses at levels 5 and 10
- Bunker hub, equipment, skills, resources and incremental progression
- Godot 4, GDScript, Compatibility renderer

## First playable milestone
Build one polished side-scrolling test area before expanding content:
- One playable class with masculine/feminine presentation
- One weapon family and its complete moveset
- Basic automatic attack, manual combo input and one active ability
- One standard enemy and one boss encounter
- Health, damage, rewards and a minimal equipment screen
- Touch controls and local save

The five-class roster, all weapon families, visual equipment system and the 100-world campaign are the full-game vision. They will be implemented in stages and validated on the target Android phone.

## Getting started
Open this project in Godot 4 and run `scenes/main.tscn`.

See [docs/GDD.md](docs/GDD.md) for the current design baseline.

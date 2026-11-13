# NEXA: WASTELAND

**Android-first 2D side-scrolling Action RPG** with incremental progression, automatic basic attacks, player-triggered combos and active abilities. Built with Godot 4 and GDScript.

## Project status
Early development — design updated to GDD v0.3. The repository currently contains a bootstrap screen, not a playable combat prototype.

## Vision
After **The Collapse**, the player reactivates the abandoned NEXA-01 bunker and ventures into a hostile wasteland. Explore distinct worlds, fight enemies and bosses, collect resources and equipment, and evolve a chosen hero.

## Core design
- Side-scrolling 2D action RPG; Android first
- Five base classes, each selectable in masculine or feminine presentation
- Five premium Divine heroes inspired by mythological deities and legendary warriors; each has a fixed identity, with no gender selection
- All base-class progression begins at **Adventurer**; advancement quests occur at every 10-world milestone
- Divine heroes have higher base stats, spectacular distinct appearances, exclusive movesets and fixed signature weapons
- Divine signature weapons can gain rarity but never change their defining visual design
- Hero level, unspent skill points, coins, diamonds and equipment rarity progression are shared across heroes; each character has its own skill tree and individual build
- Diamonds are farmable in small quantities, with additional diamonds intended to be purchasable; Divine heroes are paid unlocks
- Hybrid combat: automatic basic attacks, manual weapon combos and active class abilities
- Weapon-specific movesets and 3-hit or 5-hit combo chains
- Initial weapon families: swords, scythes, dual maces and dual blades
- Equipment visibly changes the character's appearance
- Long-term campaign target: 100 distinct worlds, 10 levels each (1,000 levels); bosses at levels 5 and 10
- Parallel Tower of Challenges: one floor per campaign stage, 1,000 initial floors and extendable beyond that
- Tower rewards improve with floor progression; every 100-floor block requires a higher promotion rank to enter (exact rank names and first-entry requirement TBD)
- Bunker hub, equipment, skills, resources and incremental progression
- Godot 4, GDScript, Compatibility renderer

## First playable milestone
Build one polished side-scrolling test area before expanding content:
- One playable base class, beginning as Adventurer, with masculine/feminine presentation
- One weapon family and its complete moveset
- Basic automatic attack, manual combo input and one active ability
- One standard enemy and one boss encounter
- Health, damage, rewards and a minimal equipment screen
- Touch controls and local save

The five base classes, five premium Divine heroes, class-advancement quest chain, all weapon families, visual equipment system and 100-world campaign are the full-game vision. They will be implemented in stages and validated on the target Android phone. Divine heroes are paid unlocks with higher base stats and unique identities; campaign progress is retained when changing heroes. Pricing and detailed progression rules remain to be designed.

## Getting started
Open this project in Godot 4 and run `scenes/main.tscn`.

See [docs/GDD.md](docs/GDD.md) for the current design baseline.

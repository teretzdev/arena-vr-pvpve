# Game Database Tables - Plain English Guide

This document provides a human-readable explanation of our game's database structure. While the technical schema defines how the database is built, this guide explains what each table stores and how they work together in simple terms.

Note: This document describes the structure and purpose of each database table. The actual game data (weapons, items, NPCs, etc.) will be populated separately and is not included in this guide.

## Weapons

The weapons table stores all the weapons that players can find, buy, or use in the game.

Fields:
* weapon_id: A unique number that identifies each weapon
* name: The display name of the weapon (e.g., "Flaming Sword")
* damage_type: What kind of damage this weapon deals (e.g., "Fire", "Physical")
* base_damage: How much damage the weapon does before any modifiers
* durability: How many times the weapon can be used before breaking
* weight: How heavy the weapon is, affecting inventory capacity
* required_level: Minimum player level needed to use this weapon
* rarity: How rare the weapon is (e.g., "Common", "Legendary")
* description: A detailed text description of the weapon
* created_at: When the weapon was added to the game

Example weapon:
```
Flaming Sword
- Damage Type: Fire
- Base Damage: 50
- Durability: 100
- Required Level: 10
- Rarity: Rare
```

## NPCs

The NPCs (Non-Player Characters) table contains all characters in the game that aren't controlled by players.

Fields:
* npc_id: A unique number for each NPC
* name: The NPC's name
* level: How powerful the NPC is
* health: How much damage the NPC can take
* faction: Which group the NPC belongs to
* is_hostile: Whether the NPC attacks players by default
* location_x/y: Where the NPC can be found in the game world
* dialogue_tree_id: Links to the NPC's conversation options
* behavior_pattern: How the NPC acts (e.g., "Guard", "Merchant")
* respawn_time: How long until the NPC returns if defeated

Example NPC:
```
Village Elder
- Level: 5
- Faction: Peaceful Village
- Is Hostile: No
- Behavior: Merchant
```

## Abilities

The abilities table tracks all special moves and skills that characters can learn and use.

Fields:
* ability_id: Unique identifier for each ability
* name: What the ability is called
* type: The category of ability (e.g., "Magic", "Physical")
* damage: How much damage the ability deals
* mana_cost: How much mana it takes to use
* cooldown: How long before it can be used again
* range: How far away it can be used
* area_of_effect: How large an area it affects
* duration: How long the ability lasts
* description: What the ability does
* required_level: Level needed to learn this ability

Example ability:
```
Fireball
- Type: Magic
- Damage: 75
- Mana Cost: 50
- Range: 30
```

## Armors

The armors table contains all protective equipment players can wear.

Fields:
* armor_id: Unique identifier for each armor piece
* name: What the armor is called
* armor_type: What kind of armor it is (e.g., "Plate", "Leather")
* defense_rating: How much damage it blocks
* weight: How heavy it is
* durability: How much damage it can take before breaking
* required_level: Level needed to wear it
* rarity: How rare it is
* description: Details about the armor

Example armor:
```
Steel Plate Armor
- Type: Plate
- Defense: 100
- Required Level: 20
```

## Items

The items table stores all collectible objects that aren't weapons or armor.

Fields:
* item_id: Unique identifier for each item
* name: What the item is called
* type: What kind of item it is (e.g., "Potion", "Crafting")
* value: How much it's worth in gold
* weight: How heavy it is
* stackable: Whether multiple items can stack in one inventory slot
* max_stack: Maximum number that can stack together
* description: What the item does
* rarity: How rare it is

Example item:
```
Health Potion
- Type: Consumable
- Value: 50
- Stackable: Yes
- Max Stack: 20
```

## Character Inventory

This table tracks what items each character is carrying.

Fields:
* inventory_id: Unique identifier for each inventory entry
* character_id: Which character owns this item
* item_id: Which item they have
* quantity: How many they have
* equipped: Whether they're currently using it

Example inventory entry:
```
Character #123's Health Potions
- Item: Health Potion
- Quantity: 5
- Equipped: No
```

## Character Abilities

This table tracks which abilities each character has learned and how good they are at using them.

Fields:
* character_id: Which character has the ability
* ability_id: Which ability they know
* level: How skilled they are with this ability
* experience: Progress toward the next level

Example character ability:
```
Character #123's Fireball
- Level: 3
- Experience: 450/1000
```

## Quests

The quests table contains all available missions and tasks for players.

Fields:
* quest_id: Unique identifier for each quest
* name: What the quest is called
* description: What players need to do
* min_level: Level required to start the quest
* experience_reward: Experience gained for completing it
* gold_reward: Gold earned for completing it
* item_reward_id: Special item given as reward
* prerequisite_quest_id: Quest that must be completed first

Example quest:
```
Save the Village
- Min Level: 5
- Rewards: 1000 XP, 100 Gold
- Prerequisite: Meet the Village Elder
```

## Relationships Between Tables

These tables work together to create the game world. Characters can have multiple items in their inventory (Character_Inventory), and learn various abilities (Character_Abilities). Quests can reward items from the Items table and might require other quests to be completed first. NPCs might drop specific weapons or items when defeated, and characters need to meet level requirements to use certain weapons, armor, and abilities.

This connected structure allows for complex gameplay mechanics while keeping the data organized and efficient.

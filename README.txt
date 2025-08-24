# MAZE GAME - COMPREHENSIVE PROJECT MAPPING

## PROJECT OVERVIEW
The Maze Game is a console-based adventure game written in C#. Players navigate through mazes, collect items, interact with NPCs, and complete quests. The project consists of 4 main components with a modular architecture.

## PROJECT STRUCTURE

### 1. SOLUTION FILE
- **Maze-game.sln** - Visual Studio solution file containing all projects

### 2. MAIN PROJECTS

#### A. Maze (Main Game Application)
**Location:** `/Maze/`
**Purpose:** Core game engine and user interface

**Files:**
- `Program.cs` - Main game loop, player input handling, rendering engine
  * Main() - Entry point, menu system, game initialization
  * Game loop - Player movement, action processing, map rendering
  * PrintMaze() - Console rendering of maze with Unicode characters
  * OpenInventory() - Inventory management UI
  * SaveGame() - Game state serialization to ZIP file
  * ContinueGame() - Load saved game from ZIP file
  * LoadGame() - [NOT IMPLEMENTED] Game loading functionality
  * NewGame() - Initialize new game session
  * SetStartForPC() - Position player at starting location

- `Extensions.cs` - Extension methods for game objects
- `Tools.cs` - Utility functions for UI and game mechanics

**Subdirectories:**
- `NPCs/` - Non-Player Character implementations
  * `Clarisa.cs` - Healer NPC with health potion shop dialogue
  * `Owen.cs` - Blacksmith NPC with weapon crafting dialogue
  
- `Maps/` - Level data files
  * `mapa0.map` to `mapa10.map` - Maze layout files (1=passable, 0=wall)
  * `mapa0.state` - JSON serialized objects for level 0 (items, NPCs, doors, etc.)
  
- `Music/` - Audio assets
  * `main_theme.mp3` - Background music
  
- `Sounds/` - Sound effects
  * `finish_sound.mp3` - Level completion sound

#### B. MapModel (Shared Game Logic)
**Location:** `/MapModel/`
**Purpose:** Core game mechanics, object models, and data persistence

**Files:**
- `MapFileFunction.cs` - File I/O operations for maps and game state
  * LoadMaze() - Read maze layout from .map files
  * SaveMaze() - Write maze layout to files
  * LoadMapObjects() - Deserialize objects from JSON
  * SaveMapObjects() - Serialize objects to JSON

**Model Classes (`Model/` subdirectory):**
- `MapObject.cs` - Base class for all game objects with X,Y coordinates
- `PC.cs` - Player Character class
  * Stats: Health, Attack, Defense, Speed, Level, XP, Gold
  * TakeItem() - Pick up items from map
  * LookAround() - Examine surroundings
  * UseItem() - Interact with objects
  * EquipItem() - Equip equipment for stat bonuses
  * EarnXP() - Experience and leveling system
  
- `NPC.cs` - Non-Player Character base class
  * CurrentDialogue - Dialogue tree root
  * TalkToPlayer() - Console-based dialogue interaction
  
- `Creature.cs` - Base class for living entities (PC, NPC)
  * ResolveCombat() - Turn-based combat system
  * Fight() - Damage calculation with attack/defense
  
- `Item.cs` - Base item class with name, description, stats
- `Equipment.cs` - Equippable items (weapons, armor, etc.)
- `Consumable.cs` - Single-use items (potions, etc.)
- `Dialogue.cs` - Dialogue tree system with responses and actions
- `Door.cs` - Interactive doors (locked/unlocked, open/closed)
- `Trap.cs` - Teleportation and damage traps
- `Start.cs` - Player spawn point marker
- `Finish.cs` - Level exit with completion conditions
- `Stat.cs` - Stat boost system for equipment
- `Skill.cs` - Player skill system [MINIMAL IMPLEMENTATION]

**Interfaces:**
- `ITrap.cs` - Interface for trap mechanics

#### C. MapEditor (Level Editor - Windows Only)
**Location:** `/MapEditor/`
**Purpose:** GUI tool for creating and editing game levels
**Note:** Requires Windows Forms, not functional in Linux environment

**Files:**
- `Editor.cs` - Main editor window with maze editing capabilities
- `Program.cs` - Editor application entry point
- `EditForms/` - Specialized editing forms for different object types
  * `ItemEditForm.cs` - Item property editor
  * `EquipmentEditForm.cs` - Equipment property editor  
  * `FinishEditForm.cs` - Level completion condition editor

#### D. MapGenerator (Utility)
**Location:** `/MapGenerator/`
**Purpose:** Procedural maze generation tool

**Files:**
- `Program.cs` - Console application for generating maze files
- `Maze.cs` - Maze generation algorithms

## GAME MECHANICS

### Core Systems:
1. **Movement System** - Arrow key navigation through maze
2. **Inventory System** - Item collection and management (I key)
3. **Interaction System** - Object examination (O key) and pickup (P key)
4. **Save/Load System** - Game state persistence (F5 to save)
5. **Level System** - Progressive maze levels with completion conditions

### Player Actions:
- **Arrow Keys** - Move through maze
- **O** - Observe/Look around current location
- **P** - Pick up items at current location  
- **U** - Use/Interact with adjacent objects
- **I** - Open inventory menu
- **F5** - Save current game state

### Object Types (ObjectTypeEnum):
- PC (0) - Player Character
- NPC (1) - Non-Player Characters
- Door (2) - Interactive barriers
- Trap (3) - Hidden teleports/damage
- Item (4) - Collectible objects
- Start (5) - Level spawn point
- Finish (6) - Level exit

### Item Types (ItemTypeEnum):
- Key - Opens locked doors
- Equipment - Provides stat bonuses when equipped
- Consumable - Single-use items (potions)
- Treasure - Required for level completion

## CURRENT IMPLEMENTATION STATUS

### ✅ WORKING FEATURES:
- Maze navigation and rendering
- Inventory management
- Item pickup and examination
- Equipment system with stat bonuses
- Save game functionality
- Trap system (teleportation)
- Combat mechanics (basic)
- Audio support (background music)

### ❌ MISSING/INCOMPLETE FEATURES:
- **NPC Interaction System** - No way to talk to NPCs in main game loop
- **Dialogue Integration** - Dialogue system exists but not connected
- **Quest System** - No quest tracking or completion mechanics
- **LoadGame() Implementation** - Method throws NotImplementedException
- **Advanced Combat** - Limited enemy encounters
- **Skill System** - Minimal implementation

### 🔧 AREAS NEEDING IMPROVEMENT:
- NPC placement in levels for testing
- Interactive dialogue in main game loop
- Quest objectives and tracking
- Better error handling and input validation
- Code documentation and comments

## TECHNICAL DETAILS

### Dependencies:
- .NET 8.0
- Newtonsoft.Json (JSON serialization)
- NetCoreAudio (sound/music playback)

### File Formats:
- **.map files** - Plain text maze layout (1=passable, 0=wall)
- **.state files** - JSON serialized game objects
- **save.zip** - Compressed game save containing map, objects, and player state

### Architecture Pattern:
- Model-View separation (MapModel for logic, Maze for presentation)
- Object-oriented design with inheritance hierarchy
- Event-driven dialogue system with Action delegates
- Factory pattern for object creation during deserialization

## DEVELOPMENT ROADMAP

### Immediate Priorities:
1. Add NPC interaction key ('T' for Talk) to main game loop
2. Implement LoadGame() functionality
3. Create example quest linking Clarisa and Owen NPCs
4. Add NPCs to first level for testing

### Future Enhancements:
- Extended quest system with objective tracking
- More diverse enemy types and combat scenarios
- Improved UI with status displays
- Sound effects for actions
- Configuration system for game settings
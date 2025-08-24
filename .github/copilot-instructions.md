# Maze Game - .NET Console Applications

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

**CRITICAL BUILD REQUIREMENTS:**
- Install .NET 8.0 SDK: `apt-get update && apt-get install -y dotnet-sdk-8.0`
- Verify .NET version: `dotnet --version` (should be 8.0.119 or higher)

**Bootstrap and Build Process:**
- Restore packages: `dotnet restore` -- takes 2 seconds. Set timeout to 30 seconds.
- Clean build artifacts: `dotnet clean MapModel && dotnet clean MapGenerator && dotnet clean Maze` -- takes 2 seconds. Set timeout to 30 seconds.
- Build shared library: `dotnet build MapModel` -- takes 1.5 seconds. NEVER CANCEL. Set timeout to 60 seconds.
- Build map generator: `dotnet build MapGenerator` -- takes 3 seconds. NEVER CANCEL. Set timeout to 60 seconds.
- Build main game: `dotnet build Maze` -- takes 2 seconds. NEVER CANCEL. Set timeout to 60 seconds.
- Build all console projects: `dotnet build MapModel && dotnet build MapGenerator && dotnet build Maze` -- takes 4 seconds total. NEVER CANCEL. Set timeout to 120 seconds.

**NEVER attempt to build the full solution** with `dotnet build` - it will fail because MapEditor requires Windows Forms which is not available on Linux.

**Running Applications:**
- Main maze game: `dotnet run --project Maze`
- Map generator: `dotnet run --project MapGenerator`
- **DO NOT** attempt to run MapEditor - it is Windows Forms only and will not work on Linux.

## Project Structure

**4 Main Projects:**
- **Maze/**: Main console game application with RPG-style maze navigation
- **MapGenerator/**: Console utility to generate new maze map files
- **MapModel/**: Shared library containing game models (PC, NPCs, Items, etc.)
- **MapEditor/**: Windows Forms map editor (LINUX INCOMPATIBLE - do not build or run)

**Key Directories:**
- `Maze/Maps/`: Contains pre-built maze files (mapa0.map through mapa10.map)
- `Maze/Music/`: Contains main_theme.mp3 audio file
- `Maze/Sounds/`: Contains finish_sound.mp3 audio file

## Validation and Testing

**Manual Validation Required:**
After making any changes to the game logic, you MUST test actual functionality:

1. **Test MapGenerator:**
   ```bash
   cd /home/runner/work/Maze-game/Maze-game
   dotnet run --project MapGenerator
   # Input: 5 (width), 5 (height), 1 (generate), 2 (save), testmaze.map (filename), 3 (exit)
   # Verify: ASCII maze displays correctly and file is saved
   ```

2. **Test Main Game:**
   ```bash
   cd /home/runner/work/Maze-game/Maze-game
   dotnet run --project Maze
   # Input: N (new game), select a map number
   # Verify: Game starts, maze displays, player can move with arrow keys
   # Test movement: Arrow keys, inventory (I), look around (O), pick up items (P)
   ```

**Build Validation:**
- Always run builds individually: `dotnet build MapModel && dotnet build MapGenerator && dotnet build Maze` -- takes 4 seconds total
- Build warnings about nullable reference types are expected and can be ignored
- Build times are fast (1-3 seconds each) - if builds take longer, investigate

**Audio Dependencies:**
- Applications attempt to play audio but `mpg123: command not found` error is expected on Linux
- This error does NOT prevent the applications from running
- Game functionality works correctly without audio

## Common Development Tasks

**When modifying MapModel:**
- Always rebuild all dependent projects: `dotnet build MapModel && dotnet build MapGenerator && dotnet build Maze`
- Check that classes like PC, Item, NPC, MapObject still function correctly

**When modifying Maze game logic:**
- Test the complete player workflow: start game, navigate maze, interact with objects, save/load
- Verify all keyboard controls work: Arrow keys (movement), I (inventory), O (look), P (pick up), U (use item)

**When modifying MapGenerator:**
- Test maze generation with different sizes: small (5x5), medium (20x20), large (50x50)
- Verify saved maze files are valid and can be loaded by the main game

## Debugging and Troubleshooting

**Interactive Console Issues:**
- Applications use `Console.ReadKey()` which requires real terminal input
- Cannot be automated with input redirection - requires manual testing
- Use timeout commands to prevent hanging during automated testing: `timeout 10 dotnet run --project Maze`

**Build Failures:**
- If solution-level build fails, use individual project builds instead
- MapEditor build failure is expected on Linux - ignore and proceed with console projects
- Nullable reference warnings are expected - focus on actual errors

**Runtime Issues:**
- Missing audio player (mpg123) is expected - game still functions
- Console applications require sufficient terminal window size
- Applications expect specific file structure - ensure Maps/ directory exists with .map files

## File Locations and Structure

**Project Files:**
```
Maze-game.sln           -- Solution file (do not build directly)
MapModel/MapModel.csproj -- Shared library project
MapGenerator/MapGenerator.csproj -- Map generator console app
Maze/Maze.csproj        -- Main game console app
MapEditor/MapEditor.csproj -- Windows Forms editor (SKIP)
```

**Runtime Dependencies:**
- .NET 8.0 runtime
- Console terminal with adequate size
- Audio player (optional, missing is acceptable)

**Asset Files:**
- All map files in Maze/Maps/ are copied to output during build
- Audio files in Maze/Music/ and Maze/Sounds/ are copied to output
- No external databases or web services required

## Expected Behavior

**MapGenerator Application:**
- Prompts for width and height input
- Generates ASCII maze with borders using Unicode box characters
- Shows player start (@) and exit (!) positions
- Saves maze as text file with 1s (passable) and 0s (walls)

**Main Maze Game:**
- Displays animated welcome screen with menu options
- Loads pre-built maze files for gameplay
- Text-based RPG with inventory system, NPCs, and items
- Uses arrow keys for movement, letter keys for actions

**Build Output:**
- Executables created in bin/Debug/net8.0/ directories
- All dependencies copied automatically
- No additional deployment steps required
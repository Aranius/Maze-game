using MapModel;
using MapModel.Model;
using Maze;
using NetCoreAudio;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO.Compression;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


class Program
{
    static bool exitFlag = false;

    static int cislo_mapy;
    static bool[,] maze;
    static PC pc;
    static string vysledok;
    static List<MapObject> mapa;
    static void Main(string[] args)
    {

        Player player = new Player();
        player.Play(@"Music/main_theme.mp3");

        Thread newThread = new Thread(StarTwinkling);

        int leftMargin = (Console.WindowWidth - 40) / 2;
        int topMargin = (Console.WindowHeight - 20) / 2;
        var frame = Tools.CreateFrame(40, 20, leftMargin);

        Console.WriteLine(frame);

        Console.SetCursorPosition(leftMargin + 10, topMargin + 1);

        Tools.PrintInMiddle("Welcome to the maze game!");
        Console.WriteLine();
        Tools.PrintInMiddle("N - New game");
        Tools.PrintInMiddle("L - Load game");
        Tools.PrintInMiddle("C - Continue game");
        Tools.PrintInMiddle("Q - Quit game");

        bool validChoice = true;
        newThread.Start();
            
        while (validChoice)
        {


            var choice = Console.ReadKey();

            switch (choice.Key)
            {
                case ConsoleKey.N:
                    NewGame(out cislo_mapy, out maze, out pc, out mapa);
                    validChoice = false;
                    break;
                case ConsoleKey.L:
                    LoadGame();
                    break;
                case ConsoleKey.C:
                    ContinueGame();
                    validChoice = false;
                    break;
                case ConsoleKey.Q:
                    Console.Clear();
                    return;

                default:
                    break;
            }
        }

        exitFlag = true;
        player.Stop();
        vysledok =string.Empty;

        while (true)
        {
            Console.Clear();

            // 4. Print the maze
            PrintMaze(maze, pc.X, pc.Y, maze.GetLength(0) -1, maze.GetLength(1)-1, mapa);

            Console.WriteLine("Use arrow keys to move!");
            Console.WriteLine("Use O to observe.");
            Console.WriteLine("Use P to pickup item.");
            Console.WriteLine("Use U to use item.");
            Console.WriteLine("Use T to talk to NPCs.");
            Console.WriteLine("Use I to open inventory.");
            Console.WriteLine("Use Q to view quest log.");
            Console.WriteLine("F5 to save game.");
            Console.WriteLine(vysledok);
            vysledok = string.Empty;
            var input = Console.ReadKey();

            // 5. Move the player
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (pc.X > 0 && maze[pc.X - 1, pc.Y])
                    {
                        pc.X--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pc.X < maze.GetLength(0) - 1 && maze[pc.X + 1, pc.Y])
                    {
                        pc.X++;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (pc.Y > 0 && maze[pc.X, pc.Y - 1])
                    {
                        pc.Y--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (pc.Y < maze.GetLength(1) - 1 && maze[pc.X, pc.Y + 1])
                    {
                        pc.Y++;
                    }
                    break;
                case ConsoleKey.O:
                    {
                        vysledok = pc.LookAround(mapa);
                    }
                    break;
                case ConsoleKey.P:
                    {
                        vysledok = pc.TakeItem(mapa);
                    }
                    break;
                // 6. Use item
                case ConsoleKey.U:
                    {
                        var surroundings = mapa.Where(m =>
                            (m.X == pc.X && Math.Abs(m.Y - pc.Y) == 1) ||
                            (m.Y == pc.Y && Math.Abs(m.X - pc.X) == 1)
                        );
                        if (surroundings.FirstOrDefault() == null)
                        {
                            vysledok = "V okoli nie je nic co by si pouzil.";
                            break;
                        }

                        vysledok =  pc.UseItem(surroundings.First(), maze);

                    }
                    break;
                case ConsoleKey.T:
                    {
                        vysledok = TalkToNPC();
                    }
                    break;
                case ConsoleKey.I:
                    {
                        OpenInventory();
                    }
                    break;
                case ConsoleKey.Q:
                    {
                        Console.Clear();
                        Console.WriteLine(pc.ShowQuests());
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;
                    case ConsoleKey.F5:
                    {
                        SaveGame();
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }

            MapObject currentObject = mapa.FirstOrDefault(m => m.X == pc.X && m.Y == pc.Y);

            // 7. Check if the player has stepped on a trap
            if (currentObject is Trap pasca)
            {
                vysledok = pasca.TrapTriggered(pc);
            }

            // 8. Check if the player has reached the end
            if (currentObject is Finish koniec)
            {
                var canContinue = koniec.CanAdvanceToNextLevel(pc);

                if (canContinue)
                {
                    koniec.FinishAchieved();
                    cislo_mapy++;
                    maze = MapFileFunction.LoadMaze(@$"Maps/mapa{cislo_mapy}.map");
                    mapa = MapFileFunction.LoadMapObjects(@$"Maps/mapa{cislo_mapy}.state");
                    SetStartForPC(pc, mapa);
                }
                else
                {
                    vysledok = $"Najdi {ItemTypeEnum.Treasure} a pak se vrať.";
                }
            }
        }
    }

    private static void OpenInventory()
    {
        int cursor = 0;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Inventory:");
            for (int i = 0; i<pc.Inventory.Count; i++)
            {
                Item? item = pc.Inventory[i];
                Console.WriteLine($"{(cursor == i ? "=>" : "  ")} {(item.ItemType == ItemTypeEnum.Equipment ? $"[{(item.IsEquiped(pc) ? "*" : " ")}]" : "   ")} {item.Name} {item.Description}");
            }

            Console.WriteLine();
            Console.WriteLine("Equip item: E");
            Console.WriteLine("Press ESC to exit.");

            var input = Console.ReadKey();

            // Move the cursor
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (cursor > 0)
                    {
                        cursor--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (cursor < pc.Inventory.Count -1)
                    {
                        cursor++;
                    }
                    break;
                case ConsoleKey.E:
                    switch (pc.Inventory[cursor].ItemType)
                    {
                        case ItemTypeEnum.Equipment:
                            var eq = pc.Inventory[cursor] as Equipment;
                            pc.EquipItem(eq);
                            break;
                        case ItemTypeEnum.Consumable:
                            pc.UseItem((Consumable)pc.Inventory[cursor], maze);
                            break;
                    }
                    break;

                case ConsoleKey.Escape:
                    return;

            }
        }
         
    }

    private static string TalkToNPC()
    {
        // Look for NPCs at the player's current location or adjacent to the player
        var npcAtLocation = mapa.FirstOrDefault(m => m.X == pc.X && m.Y == pc.Y && m is NPC) as NPC;
        
        if (npcAtLocation != null)
        {
            Console.Clear();
            Console.WriteLine("=== CONVERSATION ===");
            npcAtLocation.TalkToPlayer(pc);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return $"You talked to an NPC.";
        }
        
        // Check for NPCs in adjacent cells
        var adjacentNPC = mapa.FirstOrDefault(m => 
            m is NPC && 
            ((m.X == pc.X && Math.Abs(m.Y - pc.Y) == 1) ||
             (m.Y == pc.Y && Math.Abs(m.X - pc.X) == 1))) as NPC;
             
        if (adjacentNPC != null)
        {
            Console.Clear();
            Console.WriteLine("=== CONVERSATION ===");
            adjacentNPC.TalkToPlayer(pc);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return $"You talked to an NPC.";
        }
        
        return "There is no one to talk to here.";
    }

    private static void SaveGame()
    {
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        };

        using Stream stream = new FileStream("save.zip", FileMode.Create);
        ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Create);
        
        zip.CreateEntryFromFile($"Maps/mapa{cislo_mapy}.map", $"mapa.map");

        var aktualny_state = zip.CreateEntry("actual_state.state");        
        using (StreamWriter writer = new StreamWriter(aktualny_state.Open()))
        {
            writer.Write(JsonConvert.SerializeObject(mapa, serializerSettings));
        }
        
        var pc_state = zip.CreateEntry("pc.state");
        using (StreamWriter writer = new StreamWriter(pc_state.Open()))
        {
            writer.Write(JsonConvert.SerializeObject(pc, serializerSettings));
        }

        var level = zip.CreateEntry("level.txt");
        using (StreamWriter writer = new StreamWriter(level.Open()))
        {
            writer.WriteLine(cislo_mapy);
            writer.WriteLine(maze.GetLength(0));
            writer.WriteLine(maze.GetLength(1));
        }


        zip.Dispose();
    }

    class Star
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int TimeToLive { get; set; }
    }

    static void StarTwinkling()
    {
        List<Star> stars = new List<Star>();
        Random rand = new Random();

        while (!exitFlag)
        {
            // Generate a random position for the star
            int left = rand.Next(0, Console.WindowWidth);
            int top = rand.Next(0, Console.WindowHeight);

            // Check if the position is in the excluded area
            if (left >= 38 && left <= 77 && top >= 0 && top <= 19)
            {
                // This position is in the excluded area, skip this iteration
                continue;
            }

            // Add the star to the list
            stars.Add(new Star { Left = left, Top = top, TimeToLive = rand.Next(1000, 10000) });

            // Show all stars
            foreach (var star in stars)
            {
                Console.SetCursorPosition(star.Left, star.Top);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("*");
            }

            // Wait for a random time before the next iteration
            Thread.Sleep(500);

            foreach (var star in stars)
            {
                star.TimeToLive -= 500;
            }

            var startToRemove = stars.Where(s => s.TimeToLive <= 0).ToList();
            foreach (var star in startToRemove)
            {
                Console.SetCursorPosition(star.Left, star.Top);
                Console.Write(" ");
                stars.Remove(star);
            }

        }
    }

    private static void NewGame(out int cislo_mapy, out bool[,] maze, out PC pc, out List<MapObject> mapa)
    {
        // 1. Create a new maze

        cislo_mapy =0;
        maze =MapFileFunction.LoadMaze(@$"Maps/mapa{cislo_mapy}.map");

        // 2. Create a new player
        pc =new PC();

        mapa =MapFileFunction.LoadMapObjects(@$"Maps/mapa{cislo_mapy}.state");
        
        SetStartForPC(pc, mapa);
        
        // Initialize example quest
        InitializeQuests(pc);
    }

    private static void InitializeQuests(PC pc)
    {
        // Create Clarisa's herb gathering quest
        var herbQuest = new Quest("Herb Gathering", "Clarisa the healer needs 2 Healing Herbs to make potions for the town.");
        herbQuest.RewardGold = 25;
        herbQuest.RewardXP = 50;
        
        var gatherHerbsObjective = new QuestObjective("Collect 2 Healing Herbs", QuestObjectiveType.CollectItem)
        {
            TargetItemName = "Healing Herb",
            TargetCount = 2
        };
        
        herbQuest.Objectives.Add(gatherHerbsObjective);
        
        pc.AddQuest(herbQuest);
        
        Console.WriteLine("\n=== QUEST RECEIVED ===");
        Console.WriteLine("Clarisa the healer has given you a quest!");
        Console.WriteLine("Find 2 Healing Herbs scattered around the maze.");
        Console.WriteLine("Use 'Q' to check your quest progress.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void ContinueGame()
    {
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        if (File.Exists("save.zip"))
        {
            int sirka;
            int vyska;

            using Stream stream = new FileStream("save.zip", FileMode.Open);
            ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Read);
            var level = zip.GetEntry("level.txt");
            using (StreamReader reader = new StreamReader(level.Open()))
            {
                cislo_mapy = int.Parse(reader.ReadLine());
                vyska = int.Parse(reader.ReadLine());
                sirka = int.Parse(reader.ReadLine());
            }

            var map = zip.GetEntry("mapa.map");
            using (StreamReader reader = new StreamReader(map.Open()))
            {
                maze = new bool[vyska, sirka];
                for (int i = 0; i < vyska; i++)
                {
                    var line = reader.ReadLine();
                    for (int j = 0; j < sirka; j++)
                    {
                        maze[i, j] = line[j] == '1';
                    }
                }
            }

            var actual_state = zip.GetEntry("actual_state.state");
            using (StreamReader reader = new StreamReader(actual_state.Open()))
            {
                mapa = JsonConvert.DeserializeObject<List<MapObject>>(reader.ReadToEnd(), serializerSettings);
            }

            var pc_state = zip.GetEntry("pc.state");
            using (StreamReader reader = new StreamReader(pc_state.Open()))
            {
                pc = JsonConvert.DeserializeObject<PC>(reader.ReadToEnd());
            }
        }
        else
        {
            Console.WriteLine("No save file found.");
        }
    }

    private static void LoadGame()
    {
        // LoadGame is essentially the same as ContinueGame since we only have one save slot
        // In the future, this could be extended to support multiple save files
        ContinueGame();
    }

    private static void SetStartForPC(PC pc, List<MapObject> mapa)
    {
        var start = mapa.FirstOrDefault(m => m.ObjectType == ObjectTypeEnum.Start);
        if (start != null)
        {
            pc.X = start.X;
            pc.Y = start.Y;
        }
        else
        {
            pc.X = 0;
            pc.Y = 0;
        }
    }

    /// <summary>
    /// Prints the maze to the console.
    /// </summary>
    /// <param name="maze">The maze to print.</param>
    /// <param name="y_act">The actual Y position of the player.</param>
    /// <param name="x_act">The actual X position of the player.</param>
    /// <param name="y_ciel">The Y position of the end.</param>
    /// <param name="x_ciel">The X position of the end.</param>
    /// <param name="mapa">The map of the maze.</param>
    static void PrintMaze(bool[,] maze, int y_act, int x_act, int y_ciel, int x_ciel, List<MapObject> mapa)
    {
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        // Print the top border
        Console.Write("┌");
        for (int i = 0; i < cols; i++)
        {
            Console.Write("─");
        }
        Console.WriteLine("┐");

        // Print the maze contents
        for (int y = 0; y < rows; y++)
        {
            Console.Write("│");
            for (int x = 0; x < cols; x++)
            {
                if (x==x_act && y==y_act)
                {
                    Console.Write("@");
                    continue;
                }
                if (x==x_ciel && y==y_ciel)
                {
                    Console.Write("!");
                    continue;
                }

                MapObject currentObject = mapa.FirstOrDefault(m => m.X == y && m.Y == x);

                if (currentObject is Door dvere)
                {
                    if (dvere.Closed)
                    {
                        Console.Write("#");
                        maze[y, x] = false;
                    }
                    else
                    {
                        Console.Write(' ');
                        maze[y, x] = true;
                    }
                    continue;
                }

                if (maze[y, x])
                {
                    if (currentObject is not null)
                    {
                        if (currentObject is Item)
                        {
                            Console.Write(".");
                        } 
                        else if (currentObject is Trap pasca)
                        {
                            Console.Write(pasca.IsActive ? " " : "X");
                        }
                        else
                        {
                            Console.Write(" ");
                        }

                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                else
                {

                    bool north = y == 0 ? false : maze[y - 1, x];
                    bool south = y == rows - 1 ? false : maze[y + 1, x];
                    bool west = x == 0 ? false : maze[y, x - 1];
                    bool east = x == cols -1 ? false : maze[y, x + 1];

                    if ((!north && !south && west && east) || (!north && south && west && east) || (north && !south && west && east))
                    {
                        Console.Write("│");
                    }
                    else if (north && south && !west && !east)
                    {
                        Console.Write("─");
                    }
                    else if (!north && south && !west && east)
                    {
                        Console.Write("┘");
                    }
                    else if (north && !south && !west && east)
                    {
                        Console.Write("┐");
                    }
                    else if (north && !south && west && !east)
                    {
                        Console.Write("┌");
                    }
                    else if (!north && south && west && !east)
                    {
                        Console.Write("└");
                    }
                    else if (!north && south && !west && !east)
                    {
                        Console.Write("┴");
                    }
                    else if (north && !south && !west && !east)
                    {
                        Console.Write("┬");
                    }
                    else if (!north && !south && !west && east)
                    {
                        Console.Write("┤");
                    }
                    else if (!north && !south && west && !east)
                    {
                        Console.Write("├");

                    }
                    else if (north && south && !west && east)
                    {
                        Console.Write("─");
                    }
                    else if (north && south && west && !east)
                    {
                        Console.Write("─");
                    }
                    else
                    {
                        Console.Write("┼");
                    }
                }
            }
            Console.WriteLine("│");
        }

        // Print the bottom border
        Console.Write("└");
        for (int i = 0; i < cols; i++)
        {
            Console.Write("─");
        }
        Console.WriteLine("┘");
    }
}

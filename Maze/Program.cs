using MapModel;
using Maze;
using Maze.Model;
using NetCoreAudio;
using Newtonsoft.Json;
using System;
using System.Data;
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
        player.Play(@"Music\main_theme.mp3");

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
                    maze = MapFileFunction.LoadMaze(@$"Maps\mapa{cislo_mapy}.map");
                    mapa = MapFileFunction.LoadMapObjects(@$"Maps\mapa{cislo_mapy}.state");
                    SetStartForPC(pc, mapa);
                }
                else
                {
                    vysledok = $"Najdi {ItemTypeEnum.Treasure} a pak se vrať.";
                }
            }
        }
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
        maze =MapFileFunction.LoadMaze(@$"Maps\mapa{cislo_mapy}.map");

        // 2. Create a new player
        pc =new PC();

        mapa =MapFileFunction.LoadMapObjects(@$"Maps\mapa{cislo_mapy}.state");
        SetStartForPC(pc, mapa);
    }

    private static void ContinueGame()
    {
        throw new NotImplementedException();
    }

    private static void LoadGame()
    {
        throw new NotImplementedException();
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

                        if (currentObject is Trap pasca)
                        {
                            Console.Write(pasca.IsActive ? " " : "X");
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

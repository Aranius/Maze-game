﻿using MapModel;
using Maze.Model;
using NetCoreAudio;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Sockets;
using System.Runtime.CompilerServices;


class Program
{
static void Main(string[] args)
    {
        // 1. Create a new maze

        int cislo_mapy = 0;

        var maze = MapFileFunction.LoadMaze(@$"Maps\mapa{cislo_mapy}.map");

        // 2. Create a new player
        PC pc = new PC();
        string vysledok = string.Empty;

        var mapa = MapFileFunction.LoadMapObjects(@$"Maps\mapa{cislo_mapy}.state");

        SetStartForPC(pc, mapa);

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

                MapObject currentObject = mapa.FirstOrDefault(m=> m.X == y && m.Y == x);

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

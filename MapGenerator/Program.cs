public class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Zadaj sirku: ");
        int sirka = int.Parse(Console.ReadLine());
        Console.Write("Zadaj vysku: ");
        int vyska = int.Parse(Console.ReadLine());


        while (true)
        {
            Console.Clear();
            var maze = new Maze(sirka, vyska);
            var bludisko = maze.Generate();
            PrintMaze(bludisko, 0, 0, vyska - 1, sirka - 1);


            Console.WriteLine("1. Vygeneruj bludisko");
            Console.WriteLine("2. Uloz Bludisko");
            Console.WriteLine("3. Ukonci program");
            Console.Write("Zadaj volbu: ");
            var volba = Console.ReadKey();

            switch (volba.Key)
            {
                case ConsoleKey.D1:
                    continue;
                case ConsoleKey.D2:
                    SaveMaze(bludisko);
                    return;
                case ConsoleKey.D3:
                    return;
            }
            Console.ReadKey();
        }

    }

    private static void SaveMaze(bool[,] bludisko)
    {
        Console.Write("Zadaj nazov suboru: ");
        var nazov = Console.ReadLine();

        TextWriter subor = new StreamWriter(nazov);

        for (int i = 0; i < bludisko.GetLength(0); i++)
        {
            for (int j = 0; j < bludisko.GetLength(1); j++)
            {
                subor.Write(bludisko[i, j] ? "1" : "0");
            }
            subor.WriteLine();
        }

        subor.Close();
    }

    static void PrintMaze(bool[,] maze, int y_act, int x_act, int y_ciel, int x_ciel)
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

                if (maze[y, x])
                {
                   Console.Write(" ");
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
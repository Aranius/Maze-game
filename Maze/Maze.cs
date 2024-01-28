using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class Maze
    {
        // The grid that represents the maze, and a separate grid to keep track of visited cells
        private bool[,] grid;
        private bool[,] visited;
        private int width, height;

        // Constructor that initializes the maze dimensions and the grids
        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
            grid = new bool[width, height]; // Initially all walls (false)
            visited = new bool[width, height]; // Initially all unvisited (false)
        }

        // The main function that generates the maze
        public bool[,] Generate()
        {
            DFS(0, 0); // Start the Depth-First Search at the cell (1,1)
            grid[0, 0] = true; // Mark the start point as a passage
            grid[width - 1, height - 1] = true; // Mark the exit point as a passage
            return grid; // Return the final maze
        }

        // The recursive Depth-First Search function
        private void DFS(int x, int y)
        {
            visited[x, y] = true; // Mark the current cell as visited

            // For each neighbor in a random order
            foreach (var (nx, ny) in GetNeighbors(x, y))
            {
                // If the neighbor is within the grid and unvisited
                if (nx >= 0 && ny >= 0 && nx < width && ny < height && !visited[nx, ny])
                {
                    // Carve a path to the neighbor
                    grid[x, y] = grid[nx, ny] = true;
                    // If we're moving horizontally or vertically, we also need to carve a path through the wall between the current cell and the neighbor
                    if (x == nx)
                    {
                        grid[x, (y + ny) / 2] = true; // Carve a path in the y direction
                    }
                    else
                    {
                        grid[(x + nx) / 2, y] = true; // Carve a path in the x direction
                    }

                    // Recurse into the neighbor
                    DFS(nx, ny);
                }
            }
        }

        // Function to get the neighbors of a cell
        private List<(int, int)> GetNeighbors(int x, int y)
        {
            // The four possible directions to go (up, down, left, right)
            // We're moving two steps in each direction because we're considering each "cell" to be a 2x2 block: the actual cell plus the walls around it
            var directions = new List<(int, int)> { (-2, 0), (2, 0), (0, -2), (0, 2) };
            var neighbors = new List<(int, int)>();

            // For each direction
            foreach (var (dx, dy) in directions)
            {
                int nx = x + dx, ny = y + dy;
                // If the neighbor is within the grid
                if (nx >= 0 && ny >= 0 && nx < width && ny < height)
                {
                    neighbors.Add((nx, ny)); // Add it to the list of neighbors
                }
            }

            // Randomize the order of the neighbors
            var random = new Random();
            neighbors = neighbors.OrderBy(a => random.Next()).ToList();
            return neighbors;
        }
    }


}

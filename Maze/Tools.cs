using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public class Tools
    {
        public static void PrintInMiddle(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }

        public static string CreateFrame(int width, int height, int leftMargin)
        {

            var lines = new string[height];
            string topAndBottom = new string('*', width);
            topAndBottom = topAndBottom.PadLeft(leftMargin + width -2);

            string inner = "*" + new string(' ', width - 2) + "*";

            lines[0] = topAndBottom;
            for (int i = 1; i < height - 1; i++)
            {
                lines[i] = inner.PadLeft(leftMargin + width - 2);
            }
            lines[height - 1] = topAndBottom;

            return string.Join("\n", lines);

        }

    }
}


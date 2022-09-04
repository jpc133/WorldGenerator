using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Noise
{
    internal class NoisePrinter
    {
        public static void PrintToConsole(double[,] map, bool inverse = false)
        {
            double maxValue = 0;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] > maxValue)
                    {
                        maxValue = map[x, y];
                    }
                }
            }

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    int val = (int)((map[x, y] * 23) / maxValue);
                    if (inverse)
                    {
                        Console.Write(MapColorCode(231 + (23 - val)) + "█" + "\u001b[0m");
                    }
                    else
                    {
                        Console.Write(MapColorCode(232 + val) + "█" + "\u001b[0m");
                    }
                }
                Console.WriteLine();
            }
        }
        private static string MapColorCode(int code)
        {
            return "\u001b[38;5;" + code + "m";
        }
    }
}

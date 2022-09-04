using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Tests
{
    internal static class Utilities
    {
        internal static bool[,] GetMapFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            bool[,] initialConditions = new bool[lines[0].Length, lines.Length];
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    initialConditions[x, y] = lines[y].Substring(x, 1) == "1";
                }
            }
            return initialConditions;
        }

        internal static string GetStringOfMap(bool[,] map)
        {
            List<string> result = new();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                string line = "";
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    line += map[x, y] ? "1" : "0";
                }
                result.Add(line);
            }
            return string.Join("\n", result);
        }
    }
}

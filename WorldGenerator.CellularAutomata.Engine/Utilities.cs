using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Engine
{
    public static class Utilities
    {
        public static bool[,] GetMapFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            bool[,] initialConditions = new bool[lines[0].Length, lines.Length];
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    switch(lines[y].Substring(x, 1))
                    {
                        case "0":
                            initialConditions[x, y] = false;
                            break;
                        case "1":
                            initialConditions[x, y] = true;
                            break;
                        default:
                            throw new Exception("Unexpected character, use 'GetMapFromFileWithNulls' if you want to have nulls");
                    }
                }
            }
            return initialConditions;
        }

        public static bool?[,] GetMapFromFileWithNulls (string path)
        {
            string[] lines = File.ReadAllLines(path);
            bool?[,] initialConditions = new bool?[lines[0].Length, lines.Length];
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    switch (lines[y].Substring(x, 1))
                    {
                        case "0":
                            initialConditions[x, y] = false;
                            break;
                        case "1":
                            initialConditions[x, y] = true;
                            break;
                        case "2":
                            initialConditions[x, y] = null;
                            break;
                    }
                }
            }
            return initialConditions;
        }

        public static string GetStringOfMap(bool[,] map)
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

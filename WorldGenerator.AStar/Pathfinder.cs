using WorldGenerator.Geometry;

namespace WorldGenerator.AStar
{
    // Class Cell, with the cost to reach it, the values g and f, and the Point
    // of the cell that precedes it in a possible path
    internal class Cell
    {
        public int traversalCost;
        public int distanceFromStart;
        public int currentMinimumDistanceFromStart;
        public Point? parent;
    }
    
    internal static class PointListExtension
    {
        internal static void AddIfNotWithin(this List<Point> points, Point point)
        {
            if (!points.Contains(point))
            {
                points.Add(point);
            }
        }
    }

    public class Pathfinder
    {
        private readonly Cell[,] cells;
        private readonly int[] mapSize;

        //0, land
        //1, road
        //2, tree
        //3, rock
        //4, building
        private readonly Dictionary<int, int> weightMapping = new() { { 0, 10 }, { 1, 1 }, { 2, 100 }, { 3, 200 }, { 4, 1000 } };

        public Pathfinder(int[,] map)
        {
            mapSize = new int[] { map.GetLength(0), map.GetLength(1) };
            cells = new Cell[mapSize[0], mapSize[1]];
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    cells[x, y] = new Cell
                    {
                        traversalCost = weightMapping[map[x, y]]
                    };
                }
            }
        }

        public List<Point> FindPath(Point pathStart, Point pathEnd)
        {
            // The list of the opened cells
            List<Point> opened = new();
            // The list of the closed cells
            List<Point> closed = new();

            // Adding the start cell on the list opened
            opened.Add(pathStart);

            // Loop until the list opened is empty or a path is found
            do
            {
                // The next cell analyzed
                Point currentCell = ShorterExpectedPath(cells, opened);
                // The list of cells reachable from the actual one
                List<Point> neighbors = NeighborsCells(currentCell);
                foreach (Point newCell in neighbors)
                {
                    // If the cell considered is the final one
                    if (newCell == pathEnd)
                    {
                        cells[newCell.x, newCell.y].distanceFromStart = cells[currentCell.x, currentCell.y].distanceFromStart + cells[newCell.x, newCell.y].traversalCost;
                        cells[newCell.x, newCell.y].parent = new Point(currentCell.x, currentCell.y);
                        return TracePath(pathEnd);
                    }
                    // If the cell considered is not between the open and closed ones
                    else if (!opened.Contains(newCell) && !closed.Contains(newCell))
                    {
                        cells[newCell.x, newCell.y].distanceFromStart = cells[currentCell.x, currentCell.y].distanceFromStart + cells[newCell.x, newCell.y].traversalCost;
                        cells[newCell.x, newCell.y].currentMinimumDistanceFromStart = cells[newCell.x, newCell.y].distanceFromStart + Heuristic(newCell, pathEnd);
                        cells[newCell.x, newCell.y].parent = new Point(currentCell.x, currentCell.y);
                        opened.Add(newCell);
                    }
                    // If the cost to reach the considered cell from the actual one is
                    // smaller than the previous one
                    else if (cells[newCell.x, newCell.y].distanceFromStart > cells[currentCell.x, currentCell.y].distanceFromStart + cells[newCell.x, newCell.y].traversalCost)
                    {
                        cells[newCell.x, newCell.y].distanceFromStart = cells[currentCell.x, currentCell.y].distanceFromStart + cells[newCell.x, newCell.y].traversalCost;
                        cells[newCell.x, newCell.y].currentMinimumDistanceFromStart = cells[newCell.x, newCell.y].distanceFromStart + Heuristic(newCell, pathEnd);
                        cells[newCell.x, newCell.y].parent = new Point(currentCell.x, currentCell.y);
                        opened.AddIfNotWithin(newCell);
                        closed.Remove(newCell);
                    }
                }
                closed.AddIfNotWithin(currentCell);
                opened.Remove(currentCell);
            } while (opened.Count > 0);

            throw new Exception("No path found");
        }

        // It select the cell between those in the list opened that have the smaller
        // value of f
        private static Point ShorterExpectedPath(Cell[,] cells, List<Point> opened)
        {
            Point shorterExpectedPath = opened.First();
            for (int i = 1; i < opened.Count; i++)
            {
                if (cells[opened[i].x, opened[i].y].currentMinimumDistanceFromStart < cells[shorterExpectedPath.x, shorterExpectedPath.y].currentMinimumDistanceFromStart)
                {
                    shorterExpectedPath = new Point(opened[i].x, opened[i].y);
                }
            }
            return shorterExpectedPath;
        }

        // It finds che cells that could be reached from c
        private List<Point> NeighborsCells(Point c)
        {
            List<Point> lc = new();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != j && !(i == 1 && j == -1) && !(i == -1 && j == 1))
                    {
                        bool withinXBounds = c.x + i >= 0 && c.x + i < mapSize[0];
                        bool withinYBounds = c.y + j >= 0 && c.y + j < mapSize[1];
                        if (withinXBounds && withinYBounds && (i != 0 || j != 0))
                        {
                            lc.Add(new Point(c.x + i, c.y + j));
                        }
                    }
                }
            }
            return lc;
        }

        private List<Point> TracePath(Point end)
        {
            List<Point> path = new();
            Point currentCell = end;
            // Follow parents and add each one to path
            while (cells[currentCell.x, currentCell.y].parent != null)
            {
                path.Add((Point)cells[currentCell.x, currentCell.y].parent!);
                currentCell = (Point)cells[currentCell.x, currentCell.y].parent!;
            }
            return path;
        }

        // The function Heuristic, estimated shortest path
        private static int Heuristic(Point cell, Point finishPoint)
        {
            int dRow = Math.Abs(finishPoint.x - cell.x);
            int dCol = Math.Abs(finishPoint.y - cell.y);
            return dRow + dCol;
        }
    }
}
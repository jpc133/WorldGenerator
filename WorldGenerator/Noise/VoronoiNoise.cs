using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGenerator.Geometry;

namespace WorldGenerator.Noise
{
    public class VoronoiNoise
    {
        public static double[,] Generate(Random random, Point mapSize, int numOfRandomPoints, int constraint = 1)
        {
            List<Point> randomPoints = (new Point[numOfRandomPoints]).ToList();
            for (int i = 0; i < randomPoints.Count; i++)
            {
                randomPoints[i] = new(random.Next(0, mapSize.x / constraint) * constraint, random.Next(0, mapSize.y / constraint) * constraint);
            }

            double[,] distanceGrid = new double[mapSize.x, mapSize.y];
            for (int x = 0; x < distanceGrid.GetLength(0); x++)
            {
                for (int y = 0; y < distanceGrid.GetLength(1); y++)
                {
                    distanceGrid[x, y] = randomPoints.Select(ranP => ranP.Distance(new Point(x, y))).Min();
                }
            }

            return distanceGrid;
        }
    }
}

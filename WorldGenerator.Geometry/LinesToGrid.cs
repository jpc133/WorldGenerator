using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Geometry
{
    public class LinesToGrid
    {
        public static int[,] CreateGridFromLines(Line[] lines)
        {
            int[] xCoords = lines.SelectMany(line => line.Points.Select(point => point.x)).ToArray();
            int[] yCoords = lines.SelectMany(line => line.Points.Select(point => point.y)).ToArray();

            Point topLeftBound = new ( xCoords.Min(), yCoords.Min() );
            Point bottomRightBound = new ( xCoords.Max(), yCoords.Max() );

            Point positionCorrection = new ( -topLeftBound.x, -topLeftBound.y );
            int[] size = new int[] { bottomRightBound.x - topLeftBound.x + 1, bottomRightBound.y - topLeftBound.y + 1 };

            //Initialise grid, 0s will be blank and 1s will be roads
            int[,] grid = new int[size[0], size[1]];
            for (int x = 0; x < size[0]; x++)
            {
                for (int y = 0; y < size[1]; y++)
                {
                    grid[x, y] = 0;
                }
            }

            foreach (Point p in lines.SelectMany(line => line.GetAllPoints()))
            {
                grid[p.x, p.y] = 1; //Place roads
            }

            return grid;
        }
    }
}

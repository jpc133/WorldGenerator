using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Geometry
{
    public struct Line
    {
        public readonly Point[] Points;

        public Line(Point pointA, Point pointB)
        {
            Points = new Point[2] { pointA, pointB };
        }
    }

    public static class LineExtensions
    {
        /// <summary>
        /// Euclidean / Straight line length
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static double Length(this Line line)
        {
            return line.Points[0].Distance(line.Points[1]);
        }

        /// <summary>
        /// Manhattan line length
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static double ManhattanLength(this Line line)
        {
            return line.Points[0].ManhattanDistance(line.Points[1]);
        }

        /// <summary>
        /// Returns all the points along a line including nodes
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">When line isn't aligned to the X, Y axis</exception>
        public static Point[] GetAllPoints(this Line line)
        {
            List<Point> points = new();

            if (line.Points[0].x == line.Points[1].x) //Vertical line
            {
                if (line.Points[1].y > line.Points[0].y)
                {
                    for (int y = 0; y <= line.Points[1].y - line.Points[0].y; y++)
                    {
                        points.Add(new Point(line.Points[0].x, line.Points[0].y + y));
                    }
                }
                else
                {
                    for (int y = 0; y <= line.Points[0].y - line.Points[1].y; y++)
                    {
                        points.Add(new Point(line.Points[0].x, line.Points[1].y + y));
                    }
                }
            }
            else if (line.Points[0].y == line.Points[1].y) //Horizontal line
            {
                if (line.Points[1].x > line.Points[0].x)
                {
                    for (int x = 0; x <= line.Points[1].x - line.Points[0].x; x++)
                    {
                        points.Add(new Point(line.Points[0].x + x, line.Points[0].y));
                    }
                }
                else
                {
                    for (int x = 0; x <= line.Points[0].x - line.Points[1].x; x++)
                    {
                        points.Add(new Point(line.Points[1].x + x, line.Points[1].y));
                    }
                }
            }
            else
            {
                throw new NotImplementedException("Non axis aligned lines are not implemented");
            }

            return points.ToArray();
        }
    }
}

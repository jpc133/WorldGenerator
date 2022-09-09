using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Geometry
{
    public struct Point : IEquatable<Point>
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Point other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Point point && Equals(point);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(x, y).GetHashCode();
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }
    }

    public static class PointExtensions
    {
        /// <summary>
        /// Euclidean / Straight line distance
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static double Distance(this Point pointA, Point pointB)
        {
            int xDis = pointA.x > pointB.x ? pointA.x - pointB.x : pointB.x - pointA.x;
            int yDis = pointA.y > pointB.y ? pointA.y - pointB.y : pointB.y - pointA.y;
            return Math.Sqrt(Math.Pow(xDis, 2) + Math.Pow(yDis, 2));
        }

        /// <summary>
        /// Manhattan line length
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public static double ManhattanDistance(this Point pointA, Point pointB)
        {
            int xDis = pointA.x > pointB.x ? pointA.x - pointB.x : pointB.x - pointA.x;
            int yDis = pointA.y > pointB.y ? pointA.y - pointB.y : pointB.y - pointA.y;
            return xDis + yDis;
        }

        /// <summary>
        /// Returns a point offset by the given distance in given direction
        /// </summary>
        /// <param name="Point"></param>
        /// <param name="direction"></param>
        /// <param name="Distance"></param>
        /// <returns></returns>
        public static Point Offset(this Point Point, Direction direction, int Distance)
        {
            return new Point(Point.x * direction.DirectionVector[0] * Distance, Point.y * direction.DirectionVector[0] * Distance);
        }
    }
}

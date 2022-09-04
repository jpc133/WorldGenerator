using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.Geometry
{
    public enum CompassDirections
    {
        North,
        East,
        South,
        West
    }

    public struct Direction
    {
        public CompassDirections CompassDirection;
        public int[] DirectionVector;

        public Direction(CompassDirections compassDirection)
        {
            CompassDirection = compassDirection;
            switch (compassDirection)
            {
                default:
                case CompassDirections.North:
                    DirectionVector = new int[] { 0, 1 };
                    break;
                case CompassDirections.East:
                    DirectionVector = new int[] { 1, 0 };
                    break;
                case CompassDirections.South:
                    DirectionVector = new int[] { 0, -1 };
                    break;
                case CompassDirections.West:
                    DirectionVector = new int[] { -1, 0 };
                    break;
            }
        }
    }

    public static class DirectionExtensions
    {
        public static Direction Rotate90Clockwise(this Direction direction)
        {
            switch (direction.CompassDirection)
            {
                default:
                case CompassDirections.North:
                    return new Direction(CompassDirections.East);
                case CompassDirections.East:
                    return new Direction(CompassDirections.South);
                case CompassDirections.South:
                    return new Direction(CompassDirections.West);
                case CompassDirections.West:
                    return new Direction(CompassDirections.North);
            }
        }

        public static Direction Rotate90CounterClockwise(this Direction direction)
        {
            switch (direction.CompassDirection)
            {
                default:
                case CompassDirections.North:
                    return new Direction(CompassDirections.West);
                case CompassDirections.East:
                    return new Direction(CompassDirections.North);
                case CompassDirections.South:
                    return new Direction(CompassDirections.East);
                case CompassDirections.West:
                    return new Direction(CompassDirections.South);
            }
        }
    }
}

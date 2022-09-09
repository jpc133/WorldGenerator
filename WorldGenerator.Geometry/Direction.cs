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
            DirectionVector = compassDirection switch
            {
                CompassDirections.East => new int[] { 1, 0 },
                CompassDirections.South => new int[] { 0, -1 },
                CompassDirections.West => new int[] { -1, 0 },
                _ => new int[] { 0, 1 },
            };
        }
    }

    public static class DirectionExtensions
    {
        public static Direction Rotate90Clockwise(this Direction direction)
        {
            return direction.CompassDirection switch
            {
                CompassDirections.East => new Direction(CompassDirections.South),
                CompassDirections.South => new Direction(CompassDirections.West),
                CompassDirections.West => new Direction(CompassDirections.North),
                _ => new Direction(CompassDirections.East),
            };
        }

        public static Direction Rotate90CounterClockwise(this Direction direction)
        {
            return direction.CompassDirection switch
            {
                CompassDirections.East => new Direction(CompassDirections.North),
                CompassDirections.South => new Direction(CompassDirections.East),
                CompassDirections.West => new Direction(CompassDirections.South),
                _ => new Direction(CompassDirections.West),
            };
        }
    }
}

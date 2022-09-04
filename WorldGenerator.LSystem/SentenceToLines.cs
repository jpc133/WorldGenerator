using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGenerator.Geometry;

namespace WorldGenerator.LSystem
{
    //Roughly based on https://github.com/SunnyValleyStudio/procedural_town_unity/blob/master/Scripts/SimpleVisualizer.cs

    public class SentenceToLines
    {
        public enum Instructions
        {
            Save = '[',
            Load = ']',
            Draw = 'F',
            TurnRight = '+',
            TurnLeft = '-'
        }

        public static Line[] CreateLines(string sentence, int initialRoadLength = 8)
        {
            List<Line> lines = new();
            List<Point> nodes = new();
            Point currentPosition = new( 0, 0 );
            Point tempPosition;
            Direction direction = new (CompassDirections.East);

            int length = initialRoadLength;

            nodes.Add(currentPosition);

            Stack<(Point position, Direction direction, int length)> savePoints = new();
            foreach (char letter in sentence)
            {
                Instructions Instruction = (Instructions)letter;
                switch (Instruction)
                {
                    case Instructions.Save:
                        savePoints.Push((currentPosition, direction, length));
                        break;
                    case Instructions.Load:
                        var agentParameter = savePoints.Pop();
                        currentPosition = agentParameter.position;
                        direction = agentParameter.direction;
                        length = agentParameter.length;
                        break;
                    case Instructions.Draw:
                        tempPosition = currentPosition;
                        currentPosition = currentPosition.Offset(direction, length);

                        lines.Add(new Line(tempPosition, currentPosition));
                        length /= 2;
                        if(length <= 0) { length = 0; }
                        nodes.Add(currentPosition);
                        break;
                    case Instructions.TurnRight:
                        direction = direction.Rotate90Clockwise();
                        break;
                    case Instructions.TurnLeft:
                        direction = direction.Rotate90CounterClockwise();
                        break;
                    default:
                        break;
                }
            }

            return lines.ToArray();
        }
    }
}

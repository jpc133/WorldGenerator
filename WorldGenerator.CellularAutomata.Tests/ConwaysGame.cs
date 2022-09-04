using System.Collections.Generic;
using System.IO;
using WorldGenerator.CellularAutomata.Engine;
using WorldGenerator.CellularAutomata.Engine.Rules;
using Xunit;

namespace WorldGenerator.CellularAutomata.Tests
{
    public class ConwaysGame
    {
        readonly IRule[] conwaysGameRules =
        {
            new NeighbourLessThanCountRule(2, false, true),
            new NeighbourEqualToCountRule(2, true, true),
            new NeighbourEqualToCountRule(3, true, true),
            new NeighbourGreaterThanCountRule(3, false, null),
            new NeighbourEqualToCountRule(3, true, false)
        };

        [Fact]
        public void Glider()
        {
            bool[,] initialCondition = Utilities.GetMapFromFile("Glider/Glider1.txt");
            World game = new(initialCondition, conwaysGameRules);

            for (int i = 0; i < 4; i++)
            {
                string worldState = Utilities.GetStringOfMap(game.RunStep());
                string expectedState = Utilities.GetStringOfMap(Utilities.GetMapFromFile($"Glider/Glider{i+2}.txt"));
                Assert.Equal(worldState, expectedState);
            }
        }
    }
}
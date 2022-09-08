using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGenerator.CellularAutomata.Engine;
using WorldGenerator.CellularAutomata.Engine.Rules;
using Xunit;

namespace WorldGenerator.CellularAutomata.Tests
{
    public class RuleTests
    {
        [Fact]
        public void PositionBasedRule()
        {
            bool[,] initialCondition = Utilities.GetMapFromFile("RuleTests/twoDirectNeighbours.txt");
            World game = new(initialCondition, new IRule[] { new PositionBasedRule(Utilities.GetMapFromFile("RuleTests/twoDirectNeighbours.txt"), false, false, true) });

            string worldState = Utilities.GetStringOfMap(game.RunStep());
            var expectedMap = Utilities.GetMapFromFile("RuleTests/twoDirectNeighbours.txt");
            expectedMap[1, 1] = true;
            string expectedState = Utilities.GetStringOfMap(expectedMap);
            Assert.Equal(worldState, expectedState);
        }

        [Fact]
        public void PositionBasedRuleWithReflection()
        {
            bool[,] initialCondition = Utilities.GetMapFromFile("RuleTests/twoDirectNeighbours2.txt");
            World game = new(initialCondition, new IRule[] { new PositionBasedRule(Utilities.GetMapFromFile("RuleTests/twoDirectNeighbours.txt"), false, true, true) });

            string worldState = Utilities.GetStringOfMap(game.RunStep());
            var expectedMap = Utilities.GetMapFromFile("RuleTests/twoDirectNeighbours2.txt");
            expectedMap[1, 1] = true;
            expectedMap[0, 2] = true;
            string expectedState = Utilities.GetStringOfMap(expectedMap);
            Assert.Equal(worldState, expectedState);
        }

        [Fact]
        public void FourWayJunction()
        {
            bool[,] initialCondition = Utilities.GetMapFromFile("RuleTests/fourWayJunction.txt");
            World game = new(initialCondition, new IRule[] { new PositionBasedRule(Utilities.GetMapFromFile("RuleTests/fourWayJunction.txt"), false, false, true) });

            string worldState = Utilities.GetStringOfMap(game.RunStep());
            var expectedMap = Utilities.GetMapFromFile("RuleTests/fourWayJunction.txt");
            expectedMap[1, 1] = true;
            string expectedState = Utilities.GetStringOfMap(expectedMap);
            Assert.Equal(worldState, expectedState);
        }
    }
}

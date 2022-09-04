using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Engine.Rules
{
    public class NeighbourLessThanCountRule : IRule
    {
        public int RuleValue;
        public bool ResultCondition;
        public bool? PreCondition;

        public NeighbourLessThanCountRule(int ruleValue, bool resultCondition, bool? preCondition)
        {
            RuleValue = ruleValue;
            ResultCondition = resultCondition;
            PreCondition = preCondition;
        }

        public RuleResult ApplyRule(bool[,] grid)
        {
            if (PreCondition != null && grid[1, 1] != PreCondition)
            {
                return new RuleResult(false, null);
            }

            int count = 0;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (!(x == 1 && y == 1))
                    {
                        count += grid[x, y] ? 1 : 0;
                    }
                }
            }

            return count < RuleValue ? new RuleResult(true, ResultCondition) : new RuleResult(false, !ResultCondition);
        }
    }
}

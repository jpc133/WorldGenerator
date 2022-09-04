using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Engine.Rules
{
    public class NeighbourGreaterThanCountRule : IRule
    {
        public int RuleValue;
        public bool ResultCondition;
        public bool? PreCondition;

        public NeighbourGreaterThanCountRule(int ruleValue, bool resultCondition, bool? preCondition)
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
                        if (count > RuleValue)
                        {
                            return new RuleResult(true, ResultCondition);
                        }
                    }
                }
            }

            return new RuleResult(false, !ResultCondition);
        }
    }
}

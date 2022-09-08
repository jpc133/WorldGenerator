using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Engine.Rules
{
    public class PositionBasedRule : IRule
    {
        readonly bool?[,] ruleGrid;
        readonly bool allowRotations;
        readonly List<bool?[,]> ruleGridRotations = new();
        readonly bool allowReflections;
        readonly List<bool?[,]> ruleGridReflections = new();
        readonly bool ResultCondition;

        public PositionBasedRule(bool?[,] ruleGrid, bool allowRotations, bool allowReflections, bool resultCondition)
        {
            this.ruleGrid = ruleGrid;
            this.allowRotations = allowRotations;
            if (allowRotations)
            {
                ruleGridRotations = new List<bool?[,]>();
                GenerateRotations();
            }
            this.allowReflections = allowReflections;
            if (allowReflections)
            {
                ruleGridReflections = new List<bool?[,]>();
                GenerateReflections();
            }
            ResultCondition = resultCondition;
        }

        public PositionBasedRule(bool[,] ruleGrid, bool allowRotations, bool allowReflections, bool resultCondition)
        {
            this.ruleGrid = new bool?[3,3];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    this.ruleGrid[x, y] = ruleGrid[x, y];
                }
            }


            this.allowRotations = allowRotations;
            if (allowRotations)
            {
                GenerateRotations();
            }
            this.allowReflections = allowReflections;
            if (allowReflections)
            {
                GenerateReflections();
            }
            ResultCondition = resultCondition;
        }

        private void GenerateRotations()
        {
            bool?[,] tempGrid = new bool?[3,3];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    tempGrid[x,y] = ruleGrid[x,y];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                tempGrid = Rotate90Deg(tempGrid);
                ruleGridRotations.Add(tempGrid);
            }
        }

        private static bool?[,] Rotate90Deg(bool?[,] source)
        {
            bool?[,] dest = new bool?[3, 3];

            dest[0, 0] = source[0, 2];
            dest[1, 0] = source[0, 1];
            dest[2, 0] = source[0, 0];

            dest[0, 1] = source[1, 2];
            dest[1, 1] = source[1, 1];
            dest[2, 1] = source[1, 0];

            dest[0, 2] = source[2, 2];
            dest[1, 2] = source[2, 1];
            dest[2, 2] = source[2, 0];

            return dest;
        }

        private void GenerateReflections()
        {
            bool?[,] xAxis = new bool?[3, 3];
            bool?[,] yAxis = new bool?[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    xAxis[x, y] = ruleGrid[y, 2 - x];
                    yAxis[x, y] = ruleGrid[2 - y, x];
                }
            }
            ruleGridReflections.Add(xAxis);
            ruleGridReflections.Add(yAxis);
        }

        private static bool CheckPermutation(bool?[,] rule, bool[,] world)
        {
            bool matches = true;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (rule[x, y] != null && rule[x, y] != world[x, y])
                    {
                        matches = false;
                        break;
                    }
                }
            }
            return matches;
        }

        public RuleResult ApplyRule(bool[,] grid)
        {
            bool initialRun = CheckPermutation(ruleGrid, grid);
            if((!allowReflections && !allowRotations) || initialRun)
            {
                return new RuleResult(initialRun, initialRun ? ResultCondition : !ResultCondition);
            }

            if (allowRotations)
            {
                foreach (var reflection in ruleGridRotations!)
                {
                    bool runResult = CheckPermutation(reflection, grid);
                    if (runResult)
                    {
                        return new RuleResult(runResult, ResultCondition);
                    }
                }
            }

            if (allowReflections)
            {
                foreach (var reflection in ruleGridReflections!)
                {
                    bool runResult = CheckPermutation(reflection, grid);
                    if (runResult)
                    {
                        return new RuleResult(runResult, ResultCondition);
                    }
                }
            }

            return new RuleResult(false, !ResultCondition);
        }
    }
}

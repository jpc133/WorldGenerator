using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Engine.Rules
{
    public class RandomRuleInSet : IRule
    {
        readonly IRule[] rules;
        readonly int[]? ruleFrequency;
        readonly Random random;
        readonly bool onlyChooseRulesThatAppy;

        /// <param name="rules">The rules to be randomly used</param>
        /// <param name="ruleFrequency">The share that the rule will be used for, positive integers representing this e.g. [1,5] will mean the first rule applies 1/6 times and the second 5/6 times</param>
        /// <param name="random">Pass in a seeded instance of Random to be used for the random number picking</param>
        /// <param name="onlyChooseRulesThatAppy">Selects whether to only pick a random rule that applies</param>
        public RandomRuleInSet(IRule[] rules, int[]? ruleFrequency, Random random, bool onlyChooseRulesThatAppy)
        {
            this.rules = rules;
            this.ruleFrequency = ruleFrequency;
            this.random = random;
            this.onlyChooseRulesThatAppy = onlyChooseRulesThatAppy;
        }

        public RuleResult ApplyRule(bool[,] grid)
        {
            IRule? selectedRule = null;

            List<IRule> rulesToChooseFrom = new();
            if (onlyChooseRulesThatAppy)
            {
                rulesToChooseFrom = rules.Where(x => x.ApplyRule(grid).Applied).ToList();
            }
            else
            {
                rulesToChooseFrom = rules.ToList();
            }

            if (ruleFrequency != null)
            {
                int[] validFrequencies = new int[rulesToChooseFrom.Count];
                if (onlyChooseRulesThatAppy)
                {
                    for (int i = 0; i < rulesToChooseFrom.Count; i++)
                    {
                        validFrequencies[i] = ruleFrequency[Array.IndexOf(ruleFrequency, rulesToChooseFrom[i])];
                    }
                }
                else
                {
                    validFrequencies = ruleFrequency;
                }

                double index = random.NextDouble() * validFrequencies.Sum();
                int currentSum = 0;
                for (int i = 0; i < rules.Length; i++)
                {
                    currentSum += validFrequencies[i];
                    if(currentSum > index)
                    {
                        selectedRule = rules[i];
                        break;
                    }
                }
                if(selectedRule == null)
                {
                    selectedRule = rulesToChooseFrom.Last();
                }
            }
            else
            {
                if (rulesToChooseFrom.Count > 0)
                {
                    selectedRule = rulesToChooseFrom[random.Next(0, rulesToChooseFrom.Count)];
                }
                else
                {
                    return new RuleResult(false, false);
                }
            }

            if (selectedRule == null)
            {
                return new RuleResult(false, false);
            }

            return selectedRule.ApplyRule(grid);
        }
    }
}

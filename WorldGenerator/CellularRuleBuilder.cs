using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGenerator.CellularAutomata.Engine;
using WorldGenerator.CellularAutomata.Engine.Rules;

namespace WorldGenerator
{
    internal static class CellularRuleBuilder
    {
        internal static IRule[] GetRules(Random random)
        {
            IRule[] cellularRules = {
                new RandomRuleInSet(new IRule[] {
                    new PositionBasedRule( Utilities.GetMapFromFile("CellularRuleTemplates/oneNorth.txt"),true,true,true),
                    new PositionBasedRule( Utilities.GetMapFromFile("CellularRuleTemplates/threeNorth.txt"),true,true,true)
                }, 
                new int[] {20, 1}, random, false),
                new PositionBasedRule( Utilities.GetMapFromFile("CellularRuleTemplates/corner.txt"),true,true,true),
                new PositionBasedRule( Utilities.GetMapFromFile("CellularRuleTemplates/roadGap.txt"),true,true,true),
                new PositionBasedRule( Utilities.GetMapFromFile("CellularRuleTemplates/roadGap2.txt"),true,true,true),
                new PositionBasedRule( Utilities.GetMapFromFile("CellularRuleTemplates/roadGap3.txt"),true,true,true)
            };

            return cellularRules;
        }
    }
}

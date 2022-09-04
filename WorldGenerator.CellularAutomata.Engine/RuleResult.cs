using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Engine
{
    public struct RuleResult
    {
        public bool Applied;
        public bool? Result;

        public RuleResult(bool applied, bool? result)
        {
            Applied = applied;
            Result = result;
        }
    }
}

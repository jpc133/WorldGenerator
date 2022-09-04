using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.CellularAutomata.Engine
{
    public interface IRule
    {
        public RuleResult ApplyRule(bool[,] grid);
    }
}

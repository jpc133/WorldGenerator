using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.LSystem
{
    public class Rule
    {
        public char Start;
        public string[] RewrittenString;

        public Rule(char start, string rewrittenString)
        {
            Start = start;
            RewrittenString = new string[] { rewrittenString };
        }

        public Rule(char start, string[] rewrittenString)
        {
            Start = start;
            RewrittenString = rewrittenString;
        }
    }
}

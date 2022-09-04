using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGenerator.LSystem
{
    public class Generator
    {
        public Rule[] Rules;
        public string Sentence;
        public int Iterations;

        public float ChanceToIgnoreRule;
        private readonly Random? Random;

        public Generator(Rule[] rules, string initialSentence, int iterations = 3, Random? random = null, float chanceToIgnore = 0f)
        {
            if (chanceToIgnore > 0)
            {
                if (random == null)
                {
                    throw new ArgumentNullException(nameof(random), "When randomness is used a seeded Random must be provided");
                }
                if (chanceToIgnore < 0 || chanceToIgnore >= 1)
                {
                    throw new ArgumentException("Chance to ignore must be a value in the range 0 => value < 1", nameof(chanceToIgnore));
                }
            }

            Rules = rules;
            Sentence = initialSentence;
            Iterations = iterations;

            ChanceToIgnoreRule = chanceToIgnore;
            Random = random;
        }

        public string GenerateSentence()
        {
            string intermediateSentence = Sentence;
            for (int iterations = 0; iterations < Iterations; iterations++)
            {
                StringBuilder s = new();
                for (int i = 0; i < intermediateSentence.Length; i++)
                {
                    bool ruleMatched = false;
                    foreach (Rule rule in Rules)
                    {
                        if (Random != null && ChanceToIgnoreRule != 0f && Random.NextDouble() < ChanceToIgnoreRule)
                        {
                            continue;
                        }
                        if (intermediateSentence[i] == rule.Start)
                        {
                            ruleMatched = true;
                            if (Random != null)
                            {
                                s.Append(rule.RewrittenString[(int)Math.Floor(Random.NextDouble() * rule.RewrittenString.Length)]);
                            }
                            else
                            {
                                s.Append(rule.RewrittenString[0]);
                            }
                        }
                    }
                    if (!ruleMatched)
                    {
                        s.Append(intermediateSentence[i]);
                    }
                }
                intermediateSentence = s.ToString();
            }
            return intermediateSentence;
        }
    }
}

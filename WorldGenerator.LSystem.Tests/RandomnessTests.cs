using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WorldGenerator.LSystem.Tests
{
    public class RandomnessTests
    {
        [Fact(DisplayName = "Algae: the results from the same seed match")]
        public void Algae_Repeatable_Randomness()
        {
            string[] expectedResults = new string[7];

            Rule[] rules = new Rule[] { new Rule('A', "AB"), new Rule('B', "A") };

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "A", i, new Random(0), 0.3f);
                expectedResults[i] = lgenerator.GenerateSentence();
            }

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "A", i, new Random(0), 0.3f);
                Assert.Equal(expectedResults[i], lgenerator.GenerateSentence());
            }
        }

        [Fact(DisplayName = "Algae: the results different seeds don't match")]
        public void Algae_NonRepeatable_Randomness()
        {
            string[] expectedResults = new string[7];
            string[] expectedResults2 = new string[7];

            Rule[] rules = new Rule[] { new Rule('A', "AB"), new Rule('B', "A") };

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "A", i, new Random(0), 0.3f);
                expectedResults[i] = lgenerator.GenerateSentence();
            }

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "A", i, new Random(5), 0.3f);
                expectedResults2[i] = lgenerator.GenerateSentence();
            }

            Assert.NotEqual(expectedResults, expectedResults2);
        }

        [Fact(DisplayName = "Algae: Multiple rewrite rules are used")]
        public void AlgaeMultiRewriteRules()
        {
            string[] expectedResults = new string[7];

            Rule[] rules = new Rule[] { new Rule('A', "AB"), new Rule('B', "AC") };

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "A", i, new Random(0), 0.3f);
                expectedResults[i] = lgenerator.GenerateSentence();
            }

            Assert.Contains("C", String.Join("", expectedResults));
        }
    }
}

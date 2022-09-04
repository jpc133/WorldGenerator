using Xunit;

namespace WorldGenerator.LSystem.Tests
{
    public class NonRandomnessTests
    {
        //Tests based on examples on https://en.wikipedia.org/wiki/L-system

        [Fact(DisplayName = "Algae: rules with multiple rewrites only use the first rewrite")]
        public void MultipleRewriteRulesIgnored()
        {
            string[] expectedResults = { "A", "AB", "ABA", "ABAAB", "ABAABABA", "ABAABABAABAAB", "ABAABABAABAABABAABABA", "ABAABABAABAABABAABABAABAABABAABAAB" };

            Rule[] rules = new Rule[] { new Rule('A', new string[] { "AB", "ABC" }), new Rule('B', "A") };

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "A", i);
                Assert.Equal(expectedResults[i], lgenerator.GenerateSentence());
            }
        }

        [Fact]
        public void Algae()
        {
            string[] expectedResults = { "A", "AB", "ABA", "ABAAB", "ABAABABA", "ABAABABAABAAB", "ABAABABAABAABABAABABA", "ABAABABAABAABABAABABAABAABABAABAAB" };

            Rule[] rules = new Rule[] { new Rule('A', "AB"), new Rule('B', "A") };

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "A", i);
                Assert.Equal(expectedResults[i], lgenerator.GenerateSentence());
            }
        }

        [Fact]
        public void FractalTree()
        {
            string[] expectedResults = { "0", "1[0]0", "11[1[0]0]1[0]0", "1111[11[1[0]0]1[0]0]11[1[0]0]1[0]0" };

            Rule[] rules = new Rule[] { new Rule('1', "11"), new Rule('0', "1[0]0") };

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "0", i);
                Assert.Equal(expectedResults[i], lgenerator.GenerateSentence());
            }
        }

        [Fact]
        public void KochCurve()
        {
            string[] expectedResults = { "F", "F+F-F-F+F", "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F", "F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F+F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F-F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F-F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F+F+F-F-F+F+F+F-F-F+F-F+F-F-F+F-F+F-F-F+F+F+F-F-F+F" };

            Rule[] rules = new Rule[] { new Rule('F', "F+F-F-F+F") };

            for (int i = 0; i < expectedResults.Length; i++)
            {
                Generator lgenerator = new Generator(rules, "F", i);
                Assert.Equal(expectedResults[i], lgenerator.GenerateSentence());
            }
        }
    }
}
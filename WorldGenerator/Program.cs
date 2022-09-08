using OpenSimplex;
using System.Text;
using WorldGenerator;
using WorldGenerator.CellularAutomata.Engine;
using WorldGenerator.CellularAutomata.Engine.Rules;
using WorldGenerator.Geometry;
using WorldGenerator.LSystem;
using WorldGenerator.Noise;

Random random = new(1);

int[] mapSize = { 150, 150 };

bool[,] map = (bool[,])Array.CreateInstance(typeof(bool), mapSize[0], mapSize[1]);
map[49, 49] = true;
map[75, 75] = true;


World world = new(map, CellularRuleBuilder.GetRules(random));

for (int i = 0; i < 50; i++)
{
    world.RunStep();
}

int[,] mapInt = new int[mapSize[0], mapSize[1]];
for (int x = 0; x < mapSize[0]; x++)
{
    for (int y = 0; y < mapSize[1]; y++)
    {
        mapInt[x, y] = map[x, y] ? 1 : 0;
    }
}

//Console.WriteLine(Utilities.GetStringOfMap(world.GetWorld()));

//Rule[] rules = new Rule[] { new Rule('F', new string[] { "[+F][-F]", "[+F]F[-F]", "[-F]F[+F]" }) };

//Generator lgenerator = new(rules, "[F]--F", 10, random, 0.3f);
//Line[] roadLines = SentenceToLines.CreateLines(lgenerator.GenerateSentence(), 16);
//int[,] roadsLayer = LinesToGrid.CreateGridFromLines(roadLines);
int[,] roadsLayer = mapInt;


OpenSimplexNoise openSimplexNoise = new();
double[,] treeNoise = new double[roadsLayer.GetLength(0), roadsLayer.GetLength(1)];
for (int x = 0; x < treeNoise.GetLength(0); x++)
{
    for (int y = 0; y < treeNoise.GetLength(1); y++)
    {
        double oct1 = openSimplexNoise.Evaluate(x / 20f, y / 20f);
        double oct2 = openSimplexNoise.Evaluate(x, y) / 2;
        double val = (oct1 - oct2 + 1) / 2;
        treeNoise[x, y] = val;
    }
}

int[,] baseLayer = new int[roadsLayer.GetLength(0), roadsLayer.GetLength(1)];
string[] baseMap = { " ", mapColorCode(242) + "#" + "\u001b[0m", mapColorCode(70) + "T" + "\u001b[0m" };

int[,] buildingLayer = new int[roadsLayer.GetLength(0), roadsLayer.GetLength(1)];
string[] buildingMap = { " ", "H", "O", "I" };

for (int x = 0; x < roadsLayer.GetLength(0); x++)
{
    for (int y = 0; y < roadsLayer.GetLength(1); y++)
    {
        float sample = random.NextSingle() * 1000;
        if (sample > 997)
        {
            baseLayer[x, y] = 1;
        }
        else if (treeNoise[x, y] > 0.7f)
        {
            baseLayer[x, y] = 2;
        }
        else
        {
            baseLayer[x, y] = 0;
        }

        buildingLayer[x, y] = 0;
    }
}

printWorld();

void printWorld()
{
    for (int y = 0; y < roadsLayer.GetLength(1); y++)
    {
        StringBuilder line = new();
        for (int x = 0; x < roadsLayer.GetLength(0); x++)
        {
            if (roadsLayer[x, y] == 1)
            {
                line.Append(mapColorCode(239) + "█" + "\u001b[0m");
                continue;
            }

            if (buildingLayer[x, y] == 0)
            {
                line.Append(baseMap[baseLayer[x, y]]);
                continue;
            }
            else
            {
                line.Append(buildingMap[buildingLayer[x, y]]);
                continue;
            }
        }
        Console.WriteLine(line.ToString());
    }
}

static string mapColorCode(int code)
{
    return "\u001b[38;5;" + code + "m";
}
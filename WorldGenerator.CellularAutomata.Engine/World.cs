namespace WorldGenerator.CellularAutomata.Engine
{
    public class World
    {
        private readonly int[] mapSize;
        private readonly bool[,] world;
        private readonly IRule[] rules;

        public World(bool[,] initialConditions, IRule[] rules)
        {
            mapSize = new int[2] { initialConditions.GetLength(0), initialConditions.GetLength(1) };
            world = initialConditions;
            this.rules = rules;
        }

        public bool[,] RunStep()
        {
            bool?[,] newWorld = new bool?[mapSize[0], mapSize[1]];
            for (int x = 0; x < mapSize[0]; x++)
            {
                for (int y = 0; y < mapSize[1]; y++)
                {
                    bool[,] subGrid = new bool[3,3];
                    for (int subX = -1; subX <= 1; subX++)
                    {
                        for (int subY = -1; subY <= 1; subY++)
                        {
                            if (x + subX < 0 || y + subY < 0)
                            {
                                subGrid[subX + 1, subY + 1] = false;
                            }
                            else if(x + subX >= mapSize[0] || y + subY >= mapSize[1])
                            {
                                subGrid[subX + 1, subY + 1] = false;
                            }
                            else
                            {
                                subGrid[subX + 1, subY + 1] = world[x + subX, y + subY];
                            }
                        }
                    }

                    foreach (var rule in rules)
                    {
                        RuleResult result = rule.ApplyRule(subGrid);
                        if (result.Applied)
                        {
                            newWorld[x, y] = result.Result;
                            break;
                        }
                    }

                    if (newWorld[x, y] == null) //If no rules applied we should keep the previous value
                    {
                        newWorld[x, y] = world[x, y];
                    }
                }
            }

            for (int x = 0; x < mapSize[0]; x++)
            {
                for (int y = 0; y < mapSize[1]; y++)
                {
                    world[x, y] = newWorld[x, y]!.Value;
                }
            }

            return world;
        }
    }
}
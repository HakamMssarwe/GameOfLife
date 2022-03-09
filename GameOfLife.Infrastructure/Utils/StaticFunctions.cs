using GameOfLife.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameOfLife.Infrastructure.Utils.Enums;

namespace GameOfLife.Infrastructure.Utils
{
    abstract public class StaticFunctions
    {
        public static int GetGrowthSpeedInMilliseconds(GrowthSpeed growthSpeed)
        {
            switch (growthSpeed)
            {
                case GrowthSpeed.VerySlow:
                    return 1000;
        
                case GrowthSpeed.Slow:
                    return 750;
         
                case GrowthSpeed.Normal:
                    return 500;
           
                case GrowthSpeed.Fast:
                    return 250;

                case GrowthSpeed.VeryFast:
                    return 100;


                default:
                    return 500;
            }

        }


        public static int GetPopulationInNumbers(Population population, int numberOfCells)
        {
            switch (population)
            {
                case Population.Small:
                    return (int)(numberOfCells * 0.1);

                case Population.Medium:
                    return (int)(numberOfCells * 0.25);

                case Population.Large:
                    return (int)(numberOfCells * 0.5);

                default:
                    return (int)(numberOfCells * 0.25);
            }
        }


        public static bool CellNextGenerationState(int numberOfAliveNeighboors, bool alive, GameRules gameRule)
        {
            switch (gameRule)
            {
                case GameRules.Conway:

                    if (alive)
                    {
                        if (numberOfAliveNeighboors == 2 || numberOfAliveNeighboors == 3)
                            return true;

                      return false;
                    }

                    else
                    {
                        if (numberOfAliveNeighboors == 3)
                            return true;

                      return false;
                    }

                default:
                    return false;
            }

        }


        public static IEnumerable<Cell> Shuffle(IEnumerable<Cell> source, Random rng)
        {
            Cell[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                // ... except we don't really need to swap it fully, as we can
                // return it immediately, and afterwards it's irrelevant.
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
        }
    }

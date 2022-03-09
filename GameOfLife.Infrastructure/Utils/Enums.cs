using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Infrastructure.Utils
{
    public class Enums
    {


        public enum GameRules
        {
            Conway
        }

        public enum Population
        {
            Small,
            Medium,
            Large,
        }


        public enum GrowthSpeed
        {
            Stop,
            VerySlow,
            Slow,
            Normal,
            Fast,
            VeryFast
        }

    }
}

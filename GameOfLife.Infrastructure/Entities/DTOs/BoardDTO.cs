using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameOfLife.Infrastructure.Utils.Enums;

namespace GameOfLife.Infrastructure.Entities.DTOs
{
    public class BoardDTO
    {
        public string Id { get; set; }
        public int Rows { get; set; }
        public int Columns{ get; set; }
        public GrowthSpeed GrowthSpeed { get; set; }
        public Population Population { get; set; }
        public GameRules GameRule { get; set; }
    }
}

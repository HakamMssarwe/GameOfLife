using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Infrastructure.Entities.DB
{
    public class Cell
    {
        public int RowId { get; set; }
        public int ColumnId { get; set; }
        public bool IsAlive { get; set; }
    }
}

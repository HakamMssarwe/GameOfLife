using GameOfLife.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core.IServices
{
    public interface IGameService
    {
        public Cell GetCell(int rowId, int columnId);
        public IEnumerable<Cell> GetAllCellsAsQueryable();
    }
}

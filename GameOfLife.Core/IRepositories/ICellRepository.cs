using GameOfLife.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core.IRepositories
{
    public interface ICellRepository : IRepository<Cell>
    {
        Cell GetByIdIncluded(int rowId, int colId);
        IEnumerable<Cell> GetAllAsQueryable();
    }
}

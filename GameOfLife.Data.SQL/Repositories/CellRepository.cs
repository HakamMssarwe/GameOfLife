using GameOfLife.Core.IRepositories;
using GameOfLife.Infrastructure.Entities.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Data.SQL.Repositories
{
    public class CellRepository : Repository<Cell>, ICellRepository
    {
        private ApiContext? _context { get { return Context as ApiContext; } }
        public CellRepository(ApiContext context) : base(context)
        {

        }

        public Cell GetByIdIncluded(int rowId, int columnId)
        {
            return _context.Cells.AsQueryable().Where(x => x.RowId == rowId && x.ColumnId == columnId).FirstOrDefault();
        }

        public IEnumerable<Cell> GetAllAsQueryable()
        {
            return _context.Cells.AsQueryable();
        }
    }
}

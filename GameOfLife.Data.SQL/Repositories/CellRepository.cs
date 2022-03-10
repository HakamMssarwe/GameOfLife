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

        public Cell GetById(int rowId, int columnId)
        {
            return _context.Cells.AsQueryable().Where(x => x.RowId == rowId && x.ColumnId == columnId).FirstOrDefault();
        }

        public IQueryable<Cell> GetAllAsQueryable()
        {
            return _context.Cells.AsQueryable();
        }

        public IEnumerable<Cell> GetNeighbooringCells(Cell cell)
        {
            return _context.Cells.AsQueryable().AsNoTracking().
                Where(x => x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId - 1||
                x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId ||
                x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId + 1||
                x.RowId == cell.RowId && x.ColumnId == cell.ColumnId - 1 ||
                x.RowId == cell.RowId && x.ColumnId == cell.ColumnId + 1 ||
                x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId - 1||
                x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId ||
                x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId + 1);
        }
    }
}

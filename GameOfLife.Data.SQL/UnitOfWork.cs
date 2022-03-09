using GameOfLife.Core;
using GameOfLife.Core.IRepositories;
using GameOfLife.Data.SQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Data.SQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _context;
        public UnitOfWork(ApiContext context)
        {
            this._context = context;
        }

        private IBoardRepository _boardRepository;
        private ICellRepository _cellRepository;

        public IBoardRepository Boards => _boardRepository ??= new BoardRepository(_context);
        public ICellRepository Cells => _cellRepository ??= new CellRepository(_context);


        public int Commit()
        {
            return _context.SaveChanges();
        }

    }
}

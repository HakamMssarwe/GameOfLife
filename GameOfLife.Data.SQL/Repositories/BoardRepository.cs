using GameOfLife.Core.IRepositories;
using GameOfLife.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Data.SQL.Repositories
{
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        private ApiContext? _context { get { return Context as ApiContext; } }
        public BoardRepository(ApiContext context) : base(context)
        {

        }

        public Board GetById(string id)
        {
            return _context.Boards.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Board> GetAllAsQueryable()
        {
            return _context.Boards.AsQueryable();
        }
    }

}

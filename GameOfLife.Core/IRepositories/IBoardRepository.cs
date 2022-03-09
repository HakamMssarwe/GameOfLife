using GameOfLife.Infrastructure.Entities.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core.IRepositories
{
    public interface IBoardRepository : IRepository<Board>
    {
        Board GetById(string id);
        IEnumerable<Board> GetAllAsQueryable();
    }
}

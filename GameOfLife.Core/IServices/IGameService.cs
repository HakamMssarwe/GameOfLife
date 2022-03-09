using GameOfLife.Infrastructure.Entities.DB;
using GameOfLife.Infrastructure.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core.IServices
{
    public interface IGameService
    {
        public Board GetBoardById(string boardId);
        public IEnumerable<Board> GetAllBoardsAsQueryable();
        public Cell GetCell(int rowId, int columnId);
        public IEnumerable<Cell> GetAllCellsAsQueryable();

        public bool CreateBoard(Board board);
        public bool UpdateBoard(Board board);
        public bool DeleteBoard(Board board, bool commit);
        public bool MakeRandomCellsAlive(Board board);
        public bool UpdateGeneration(string boardId);
        public bool Commit();
    }
}

using GameOfLife.Core;
using GameOfLife.Core.IServices;
using GameOfLife.Infrastructure.Entities.DB;
using GameOfLife.Infrastructure.Entities.DTOs;
using GameOfLife.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Data.Access.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Board GetBoardById(string boardId)
        {
            return _unitOfWork.Boards.GetById(boardId);
        }
        public IEnumerable<Board> GetAllBoardsAsQueryable()
        {
            return _unitOfWork.Boards.GetAllAsQueryable();
        }

        public Cell GetCell(int rowId, int columnId)
        {
            return _unitOfWork.Cells.GetById(rowId, columnId);
        }

        public IEnumerable<Cell> GetAllCellsAsQueryable()
        {
            return _unitOfWork.Cells.GetAllAsQueryable();
        }

        public bool CreateBoard(Board board)
        {
            //Create the board
            board.LastTimeUpdated = DateTime.Now;
            _unitOfWork.Boards.Add(board);
            var res =  _unitOfWork.Commit() > 0;

            //Fill the board with cells
            if (res)
            {
                for (int row = 1; row <= board.Rows; row++)
                {
                    for (int column = 1; column <= board.Columns; column++)
                    {
                        Cell newCell = new Cell
                        {
                            BoardId = board.Id,
                            RowId = row,
                            ColumnId = column
                        };

                        _unitOfWork.Cells.Add(newCell);

                    }
                }

                return _unitOfWork.Commit() > 0;
            }

            return res;

        }

        public bool UpdateBoard(Board board)
        {
            _unitOfWork.Boards.Update(board);
            return _unitOfWork.Commit() > 0;
        }

        public bool DeleteBoard(Board board, bool commit)
        {
            _unitOfWork.Boards.Remove(board);

            if (commit)
             return _unitOfWork.Commit() > 0;

            return true;

        }

        /// <summary>
        /// For better performance this function gets the cells related to the board as queryable from the database, randomizes them and at the same time when it's randomizing,using the yeild return statement will allow us
        /// to only loop through the amount of cells we want, without going through the whole table of cells.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool MakeRandomCellsAlive(Board board)
        {
            var numberOfCells = StaticFunctions.GetPopulationInNumbers(board.Population, board.Rows * board.Columns);

            var cells = _unitOfWork.Cells.GetAllAsQueryable();

            foreach (var cell in StaticFunctions.Shuffle(cells, new Random()))
            {
                cell.IsAlive = true;
                _unitOfWork.Cells.Update(cell);


                --numberOfCells;

                if (numberOfCells == 0)
                    break;
            }

            return _unitOfWork.Commit() > 0; 
        }


        public Board UpdateGeneration(string boardId)
        {
            var board = _unitOfWork.Boards.GetById(boardId);

            var aliveCells = _unitOfWork.Cells.GetAllAsQueryable().Where(x => x.BoardId == board.Id && x.IsAlive);
            var deadCells = _unitOfWork.Cells.GetAllAsQueryable().Where(x => x.BoardId == board.Id && !x.IsAlive);

            #region Handle alive Cells
            foreach (var cell in aliveCells)
            {
                int numberOfNeighbooringLiveCells = 0;

                if (aliveCells.Any(x => x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId - 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId + 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId && x.ColumnId == cell.ColumnId - 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId && x.ColumnId == cell.ColumnId + 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId - 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId))
                    numberOfNeighbooringLiveCells++;


                if (aliveCells.Any(x => x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId + 1))
                    numberOfNeighbooringLiveCells++;



                var deadOrAlive = StaticFunctions.CellNextGenerationState(numberOfNeighbooringLiveCells, true, board.GameRule);


                if (cell.IsAlive != deadOrAlive)
                {
                    cell.IsAlive = deadOrAlive;
                    _unitOfWork.Cells.Update(cell);
                }

            }

            #endregion


            #region Handle dead Cells
            foreach (var cell in deadCells)
            {
                int numberOfNeighbooringLiveCells = 0;

                if (aliveCells.Any(x => x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId - 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId - 1 && x.ColumnId == cell.ColumnId + 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId && x.ColumnId == cell.ColumnId - 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId && x.ColumnId == cell.ColumnId + 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId - 1))
                    numberOfNeighbooringLiveCells++;

                if (aliveCells.Any(x => x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId))
                    numberOfNeighbooringLiveCells++;


                if (aliveCells.Any(x => x.RowId == cell.RowId + 1 && x.ColumnId == cell.ColumnId + 1))
                    numberOfNeighbooringLiveCells++;



                var deadOrAlive = StaticFunctions.CellNextGenerationState(numberOfNeighbooringLiveCells, false, board.GameRule);


                if (cell.IsAlive != deadOrAlive)
                {
                    cell.IsAlive = deadOrAlive;
                    _unitOfWork.Cells.Update(cell);
                }

            }

            #endregion


            _unitOfWork.Commit();
            return board;
        }


        public bool Commit()
        {
            return _unitOfWork.Commit() > 0;
        }

        
    }
}

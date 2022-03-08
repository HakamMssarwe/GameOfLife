using GameOfLife.Core;
using GameOfLife.Core.IServices;
using GameOfLife.Infrastructure.Entities.DB;
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

        public Cell GetCell(int rowId, int columnId)
        {
            return _unitOfWork.Cells.GetByIdIncluded(rowId, columnId);
        }

        public IEnumerable<Cell> GetAllCellsAsQueryable()
        {
            throw new NotImplementedException();
        }


    }
}

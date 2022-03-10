using AutoMapper;
using GameOfLife.Core.IServices;
using GameOfLife.Infrastructure.Entities.DB;
using GameOfLife.Infrastructure.Entities.DTOs;
using GameOfLife.Infrastructure.Utils;
using GameOfLife.Web.API.Hubs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GameOfLife.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        readonly IMapper _mapper;
        readonly IGameService _gameService;
        readonly ILogger<GamesController> _logger;
        readonly IHubContext<GameHub> _hubContext;
        readonly IServiceScopeFactory _scopeFactory;

        public GamesController(IMapper mapper, IGameService gameService, ILogger<GamesController> logger, IHubContext<GameHub> hubContext, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _gameService = gameService;
            _logger = logger;
            _hubContext = hubContext;
            _scopeFactory = scopeFactory;
        }


        [HttpPost]
        [Route("InitateGame")]
        public IActionResult InitateGame([FromBody] BoardDTO dto)
        {
            try
            {
                var board = _mapper.Map<Board>(dto);

                var res = _gameService.CreateBoard(board);

                if (!res)
                    return BadRequest("The board has not been created.");


                res = _gameService.MakeRandomCellsAlive(board);

                if (!res)
                {
                    _gameService.DeleteBoard(board, true);
                    throw new Exception("Failed to randomize alive cells.");
                }

                var filteredCells = new List<List<object>>();

                for (int i = 1; i <= board.Rows; i++)
                {
                    var cells = _gameService.GetAllCellsAsQueryable().Where(x => x.BoardId == board.Id && x.RowId == i).Select(x => new {x.RowId, x.ColumnId, x.IsAlive});

                    var rowCells = new List<object>();

                    foreach(var cell in cells)
                        rowCells.Add(cell);
                    

                    filteredCells.Add(rowCells);
                }


                return Ok(new
                {
                    boardId = board.Id,
                    cells = filteredCells
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest("Something went wrong.");
            }
        }

        [HttpGet]
        [Route("StartGame/{boardId}")]
        public IActionResult StartGame(string boardId)
        {
            Task.Run(() =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var scopedGameService = scope.ServiceProvider.GetRequiredService<IGameService>();

                    //If the board is still connected, the Id will be stored in this list
                    while (GameHub.ConnectedBoards.Any(x => x == boardId))
                    {
                        var res = scopedGameService.UpdateGeneration(boardId);
                        var board = scopedGameService.GetBoardById(boardId);

                        if (board != null)
                        {
                            var filteredCells = new List<List<object>>();


                            for (int i = 1; i <= board.Rows; i++)
                            {
                                var cells = scopedGameService.GetAllCellsAsQueryable().Where(x => x.BoardId == board.Id && x.RowId == i).Select(x => new { x.RowId, x.ColumnId, x.IsAlive });

                                var rowCells = new List<object>();

                                foreach (var cell in cells)
                                    rowCells.Add(cell);


                                filteredCells.Add(rowCells);
                            }

                            if (res)
                                _hubContext.Clients.Client(boardId).SendAsync("UpdateGeneration", filteredCells);


                            board.LastTimeUpdated = DateTime.Now;
                            scopedGameService.UpdateBoard(board);
                            Task.Delay(StaticFunctions.GetGrowthSpeedInMilliseconds(board.GrowthSpeed)).Wait();
                        }
                    }
                }
                
            });

            return Ok("The game has started.");

        }

        [HttpPost]
        [Route("StopGame/{boardId}")]
        public IActionResult StopGame(string boardId)
        {
            try
            {
                var board = _gameService.GetBoardById(boardId);

                if (board == null)
                    return BadRequest("Board does not exist.");

                var res = _gameService.DeleteBoard(board, true);

                if (res)
                {
                    _hubContext.Clients.Client(boardId).SendAsync("DisconnectClient");
                    return Ok("The board has been deleted.");
                }


                return BadRequest("The board has not been deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest("Something went wrong.");
            }

        }
    }
}

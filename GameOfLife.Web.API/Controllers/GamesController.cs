using AutoMapper;
using GameOfLife.Core.IServices;
using GameOfLife.Infrastructure.Entities.DB;
using GameOfLife.Infrastructure.Entities.DTOs;
using GameOfLife.Infrastructure.Utils;
using GameOfLife.Web.API.Hubs;
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
        private readonly IServiceProvider _serviceProvider;

        public GamesController(IMapper mapper, IGameService gameService, ILogger<GamesController> logger, IHubContext<GameHub> hubContext, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _gameService = gameService;
            _logger = logger;
            _hubContext = hubContext;
            _serviceProvider = serviceProvider;
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


                var cells = _gameService.GetAllCellsAsQueryable().Where(x => x.BoardId == board.Id).Select(x => new { x.RowId, x.ColumnId, x.IsAlive });


                return Ok(new
                {
                    boardId = board.Id,
                    cells = cells
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest("Something went wrong.");
            }
        }

        [HttpPost]
        [Route("StartGame/{boardId}")]
        public IActionResult StartGame(string boardId)
        {
            Task.Run(() =>
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IGameService scopedGameService = scope.ServiceProvider.GetRequiredService<IGameService>();

                    //If the board is still connected, the Id will be stored in this list
                    while (GameHub.ConnectedBoards.Any(x => x == boardId))
                    {
                        var board = scopedGameService.UpdateGeneration(boardId);

                        var cells = scopedGameService.GetAllCellsAsQueryable().Where(x => x.BoardId == boardId).Select(x => new { x.RowId, x.ColumnId, x.IsAlive });

                        _hubContext.Clients.Client(board.Id).SendAsync("UpdateGeneration", cells);

                        board.LastTimeUpdated = DateTime.Now;
                        scopedGameService.UpdateBoard(board);

                        Task.Delay(StaticFunctions.GetGrowthSpeedInMilliseconds(board.GrowthSpeed));
                    }
                }
            });

            return Ok("The game has started.");
        }
    }
}

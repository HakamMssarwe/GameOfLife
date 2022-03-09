using GameOfLife.Core.IServices;
using static GameOfLife.Infrastructure.Utils.Enums;

namespace GameOfLife.Web.API.BackgroundServices
{
    public class GamesMemoryHandler : BackgroundService
    {
        readonly ILogger<GamesMemoryHandler> _logger;
        readonly IServiceProvider _services;

        public GamesMemoryHandler(ILogger<GamesMemoryHandler> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var _gameService = scope.ServiceProvider.GetRequiredService<IGameService>();

                        var boards = _gameService.GetAllBoardsAsQueryable();

                        foreach (var board in boards)
                        {
                            var timePassed = board.LastTimeUpdated - DateTime.Now;

                            if (timePassed.TotalMinutes > 5 && board.GrowthSpeed != GrowthSpeed.Stop)
                                _gameService.DeleteBoard(board, false);

                            else if (timePassed.TotalMinutes > 60)
                                _gameService.DeleteBoard(board, false);

                        }

                        _gameService.Commit();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }

                await Task.Delay((1000 * 60) * 5 , stoppingToken);
            }
        }
    }
}




using GameOfLife.Infrastructure.Entities.DB;
using Microsoft.AspNetCore.SignalR;

namespace GameOfLife.Web.API.Hubs
{


    public class GameHub : Hub
    {

        public static List<string> ConnectedBoards = new List<string>();

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public override Task OnConnectedAsync()
        {
            ConnectedBoards.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedBoards.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }



    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using StonePapperScissorLib;

namespace StonePaperCScissor.Hubs
{
    public class MainGameHub : Hub<IMainHub>
    {
        const string usrNameKey = "user_name";

        public Task SendMessage(Message message)
        {
            Console.WriteLine($"ClientId: {Context.ConnectionId}");

            object name;
            Context.Items.TryGetValue(usrNameKey, out name);

            message.Title = name as string;

            return Clients.Others.Send(message);
            
        }

        public Task SetName(string name)
        {

            Context.Items.TryAdd(usrNameKey, name);
            return Clients.Caller.SetName($"you name is: {name}" );
            //return Task.FromResult($"your name is : {name}");
        }

        public override Task OnConnectedAsync()
        {
            return Clients.Others.Send(new Message { Title= $"Client {Context.ConnectionId} connected"});
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return Clients.Others.Send(new Message { Title= $"Client {Context.ConnectionId} disconnected"});
        }
    }
}
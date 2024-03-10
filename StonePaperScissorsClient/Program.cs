using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using StonePapperScissorLib;

internal class Program
{
    static HubConnection connection;
    private static bool isNameSet = false;

    private static async Task Main(string[] args)
    {
        await InitConnectionBuilder();
        await SetupNameUser();

        while (true && isNameSet)
        {

            Console.WriteLine("Enter message: ");
            var cmd = Console.ReadLine();


            if (string.IsNullOrWhiteSpace(cmd))
                continue;

            if (cmd == "exit")
                break;

            var mess = new Message();
            mess.Body = cmd;

            await connection.SendAsync("SendMessage", mess);
        }
    }

    private static async Task SetupNameUser()
    {
        await Console.Out.WriteLineAsync("Enter your name: ");
        var name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            await Console.Out.WriteLineAsync("You need to set a name for continue");
            await SetupNameUser();
        }

        if (name == "exit")
            return;

        isNameSet = true;

       var resp = await connection.InvokeAsync<string>("SetName", name);
        await Console.Out.WriteLineAsync(resp);
    }


    private static Task InitConnectionBuilder()
    {
        connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:5001/index")
            .Build();

        connection.On<Message>("Send", (mess) => { 
            Console.WriteLine(mess.Title); 
            Console.WriteLine(mess.Body); 

        });

        connection.On<string>("SetName", (name) => Console.WriteLine(name));

        return connection.StartAsync();
    }



}
using System.Reflection;
using chatsocket;
using Fleck;
using lib;

var builder = WebApplication.CreateBuilder(args);


var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

var app = builder.Build();

var server = new WebSocketServer("ws://0.0.0.0:8181");


server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        StateService.AddConnection(ws);
    };
    ws.OnMessage = async message =>
    {
        try
        {
            await app.InvokeClientEventHandler(clientEventHandlers, ws, message);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException);
            Console.WriteLine(e.StackTrace);
        }
    };
});

Console.ReadLine();
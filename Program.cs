using Fleck;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var server = new WebSocketServer("ws://0.0.0.0:8181");
var wsConnections = new List<IWebSocketConnection>();
server.Start(socket =>
{
    socket.OnOpen = () =>
    {
        wsConnections.Add(socket);
    };
    socket.OnMessage = message =>
    {
        foreach (var webSocketConnection in wsConnections)
        {
            webSocketConnection.Send(message);
        }
    };
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
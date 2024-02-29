using System.Text.Json;
using Fleck;
using lib;

namespace chatsocket;

public class ClienWantsToEnterRoomDto : BaseDto
{
    public int roomId { get; set; }
}

public class ClientWantsToEnterRoom : BaseEventHandler<ClienWantsToEnterRoomDto>
{
    public override Task Handle(ClienWantsToEnterRoomDto dto, IWebSocketConnection socket)
    {
        var isSuccess = StateService.AddToRoom(socket, dto.roomId);
        socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom()
        {
            message = "You were successfully added" + dto.roomId
        }));
        return Task.CompletedTask;
    }
}

public class ServerAddsClientToRoom : BaseDto
{
    public string message { get; set; }
}
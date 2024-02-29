using Fleck;
using lib;
using Microsoft.AspNetCore.Authentication;

namespace chatsocket;

public class ClientWantsToSignInDto : BaseDto
{
    public string Username { get; set; }
}

public class ClientWantsToSignIn : BaseEventHandler<ClientWantsToSignInDto>
{
    public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
    {
        StateService.Connections[socket.ConnectionInfo.Id].Username = dto.Username;
        return Task.CompletedTask;
    }
}
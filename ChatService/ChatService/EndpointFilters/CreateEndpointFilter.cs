
using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Users;
using ChatService.Infrastructure.Hubs.Chat;
using ChatService.Infrastructure.Hubs.Chat.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.EndpointFilters;
public class CreateEndpointFilter : IEndpointFilter
{
    public IHubContext<ChatUserHub, IChatUserHub> _userHubContext { get; }

    public CreateEndpointFilter(IHubContext<ChatUserHub, IChatUserHub> userHubContext)
    {
        _userHubContext = userHubContext;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is ResultDTO<UserDTO> dto &&
            dto.StatusCode == System.Net.HttpStatusCode.Created &&
            ResultDTO<UserDTO>.HasValue(dto.Data))
        {
            _ = _userHubContext.Clients.All.JoinAsync(
                    new MessageDTO(
                        dto.Data,
                        $"{dto.Data.Key.Name} has join the chat.",
                        DateTime.UtcNow)
                    );

        }
        return result;
    }
}


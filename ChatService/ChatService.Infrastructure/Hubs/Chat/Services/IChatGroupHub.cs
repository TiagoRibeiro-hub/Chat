using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.GroupUser;

namespace ChatService.Infrastructure.Hubs.Chat.Services;

public interface IChatGroupHub : IChaHub
{
    Task ConnectedUsers(IEnumerable<GroupUserDTO> users);
    Task LeftGroupAsync(MessageDTO message);
    Task PreviousMessageAsync(GroupDTO groupDTO, IEnumerable<MessageDTO>? messagesListDTO);
}

using ChatService.Api.DTOS;
using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.GroupUser;
using ChatService.Infrastructure.Hubs.Chat.Services;
using ChatService.Infrastructure.Utils;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Infrastructure.Hubs.Chat;

public sealed class ChatGroupHub : Hub<IChatGroupHub>
{
    private readonly IDictionary<string, GroupUserDTO>? _groupConnections;
    private readonly IDictionary<Guid, List<MessageDTO>>? _groupMessages;

    public Task SendConnectedUserGroupDTO(GroupKeyDTO key)
    {
        key.ValidateIdentifier();

        return Clients.Group(key.Identifier.ToString())
                        .ConnectedUsers(
                            _groupConnections!.Values.Where(x => x.Group.Key.Identifier == key.Identifier)
                            );
    }

    public Task SendPreviousMessages(GroupDTO groupDTO)
    {
        // TODO Db confirm groupName
        groupDTO.Key.ValidateIdentifier();

        return Clients.Group(groupDTO.Key.Identifier.ToString())
                        .PreviousMessageAsync(
                            groupDTO,
                            _groupMessages?.GetMessagesDTO(groupDTO.Key.Identifier, null)
                            );
    }

    public async Task SendMessageAsync(string message)
    {
        if (_groupConnections!.TryGetValue(Context.ConnectionId, out GroupUserDTO? userGroupDTO))
        {
            var messageDTO = new MessageDTO(userGroupDTO.User, message, DateTime.UtcNow);
            await Clients.Group(userGroupDTO.Group.Key.Identifier.ToString()).ReceiveMessageAsync(userGroupDTO.User, messageDTO);

            _groupMessages![userGroupDTO.Group.Key.Identifier] = _groupMessages.GetMessagesDTO(userGroupDTO.Group.Key.Identifier, messageDTO);

            // TODO call Db
        }

        throw new Exception("Invalid connection");
    }

    public async Task JoinGroupAsync(GroupUserDTO userGroupDTO)
    {
        userGroupDTO.Group.Key.ValidateIdentifier();
        userGroupDTO.User.Key.ValidateIdentifier();

        List<MessageDTO>? messagesListDTO = null; // TODO Bd messages from group 
        if (messagesListDTO == null)
        {
            var messageDTO = new MessageDTO(userGroupDTO.User, "Is connected with the group", DateTime.UtcNow);
            await Clients.Group(userGroupDTO.Group.Key.Identifier.ToString()).JoinAsync(messageDTO);

            _groupMessages![userGroupDTO.Group.Key.Identifier] = _groupMessages.GetMessagesDTO(userGroupDTO.Group.Key.Identifier, messageDTO);

            // TODO Bd start a group conversation
        }
        else
        {
            await Clients.Caller.PreviousMessageAsync(userGroupDTO.Group, messagesListDTO);
            _groupMessages![userGroupDTO.Group.Key.Identifier] = messagesListDTO;
        }
    }

    public async Task LeftGroupAsync(GroupUserDTO userGroupDTO)
    {
        userGroupDTO.Group.Key.ValidateIdentifier();

        var messageDTO = new MessageDTO(userGroupDTO.User, "Is disconnected with the group", DateTime.UtcNow);

        _ = _groupConnections!.Remove(Context.ConnectionId);
        await SendConnectedUserGroupDTO(userGroupDTO.Group.Key);

        await Clients.Group(userGroupDTO.Group.Key.Identifier.ToString()).LeftGroupAsync(messageDTO);

        _groupMessages![userGroupDTO.Group.Key.Identifier] = _groupMessages.GetMessagesDTO(userGroupDTO.Group.Key.Identifier, messageDTO);

        // TODO call Db
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_groupConnections!.TryGetValue(Context.ConnectionId, out GroupUserDTO? userGroupDTO))
        {
            _ = _groupConnections.Remove(Context.ConnectionId);
            SendConnectedUserGroupDTO(userGroupDTO.Group.Key);
        }
        return base.OnDisconnectedAsync(exception);
    }
}

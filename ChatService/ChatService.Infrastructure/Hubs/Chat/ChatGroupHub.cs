using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Messages;
using ChatService.Core;
using ChatService.Core.Helpers;
using ChatService.Core.Services.GroupServices;
using ChatService.Domain.Entities;
using ChatService.Infrastructure.Hubs.Chat.Services;
using ChatService.Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Infrastructure.Hubs.Chat;

public sealed class ChatGroupHub : Hub<IChatGroupHub>
{
    private readonly IDictionary<string, GroupUserDTO>? _groupConnections;
    private readonly IDictionary<Guid, List<UserMessageDTO>>? _groupMessages;

    public Task SendConnectedUserGroupDTO(GroupKeyDTO key)
    {
        key.ValidateIdentifier();

        return Clients.Group(key.Identifier.ToString())
                        .ConnectedUsers(_groupConnections!.Values.Where(x => x.Group.Key.Identifier == key.Identifier));
    }

    public async Task SendPreviousMessages(GroupDTO groupDTO, [FromServices] IGroupService groupService)
    {
        if (Guards.IsNull(groupDTO.Key))
        {
            throw new Exception(string.Format(ErrorMessages.NotFound, nameof(GroupKeyDTO)));
        }

        groupDTO.Key.ValidateIdentifier();

        if (string.IsNullOrEmpty(groupDTO.Key.Name))
        {
            groupDTO.Key.Name = await groupService.GetNameAsync(groupDTO.Key.ToDomainKey<GroupKeyDTO, GroupKey>());
        }

        _ = Clients.Group(groupDTO.Key.Identifier.ToString())
                   .PreviousMessageAsync(groupDTO, _groupMessages?.GetMessagesDTO(groupDTO.Key.Identifier, null));
    }

    public async Task SendMessageAsync(string message)
    {
        if (_groupConnections!.TryGetValue(Context.ConnectionId, out GroupUserDTO? userGroupDTO))
        {
            var messageDTO = new UserMessageDTO(userGroupDTO.User.Key.Identifier, message, DateTime.UtcNow);
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

        var messageDTO = new UserMessageDTO(userGroupDTO.User.Key.Identifier, "Is connected with the group", DateTime.UtcNow);

        await Clients.Group(userGroupDTO.Group.Key.Identifier.ToString()).JoinAsync(messageDTO);

        //_groupMessages![userGroupDTO.Group.Key.Identifier] = _groupMessages.GetMessagesDTO(userGroupDTO.Group.Key.Identifier, messageDTO);
    }

    public async Task LeftGroupAsync(GroupUserDTO userGroupDTO)
    {
        userGroupDTO.Group.Key.ValidateIdentifier();

        var messageDTO = new UserMessageDTO(userGroupDTO.User.Key.Identifier, "Is disconnected with the group", DateTime.UtcNow);

        _ = _groupConnections!.Remove(Context.ConnectionId);

        await SendConnectedUserGroupDTO(userGroupDTO.Group.Key);

        await Clients.Group(userGroupDTO.Group.Key.Identifier.ToString()).LeftGroupAsync(messageDTO);

        //_groupMessages![userGroupDTO.Group.Key.Identifier] = _groupMessages.GetMessagesDTO(userGroupDTO.Group.Key.Identifier, messageDTO);
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

using ChatService.Api.DTOS.Messages;

namespace ChatService.Api.DTOS.UserGroups;

public sealed class GroupMessagesDTO
{
    public GroupMessagesDTO(
        Guid groupIdentifier,
        List<UserMessageDTO> messages
        )
    {
        GroupIdentifier = groupIdentifier;
        Messages = messages;
    }

    public Guid GroupIdentifier { get; set; }
    public List<UserMessageDTO> Messages { get; set; }
}

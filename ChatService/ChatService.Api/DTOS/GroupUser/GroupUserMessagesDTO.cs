using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;

namespace ChatService.Api.DTOS.UserGroups;

public sealed class GroupUserMessagesDTO
{
    public GroupUserMessagesDTO(
        GroupDTO group,
        List<UserDTO> users,
        List<MessageDTO> messages
        )
    {
        Group = group;
        Users = users;
        Messages = messages;
    }

    public GroupDTO Group { get; set; }
    public List<UserDTO> Users { get; set; }
    public List<MessageDTO> Messages { get; set; }
}

using ChatService.Api.DTOS.Groups;
using ChatService.Api.DTOS.Users;

namespace ChatService.Api.DTOS.GroupUser;

/// <summary>
/// Used for hubs
/// </summary>
/// <param name="Group"></param>
/// <param name="User"></param>
public sealed record GroupUserDTO(GroupDTO Group, UserDTO User);

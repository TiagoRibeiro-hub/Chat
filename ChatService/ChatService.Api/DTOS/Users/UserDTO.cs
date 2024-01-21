using ChatService.Api.DTOS.Groups;
using ChatService.Core.Helpers;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;

namespace ChatService.Api.DTOS.Users;

public sealed class UserDTO : BaseDTO<UserDTO, User>
{
    public UserDTO(UserKeyDTO key)
    {
        Key = key;
    }

    public UserKeyDTO Key { get; set; }
    public List<GroupDTO>? Groups { get; set; }
    public List<GroupRolesDTO>? Roles { get; set; }

    internal override void ValidateKey()
    {
        Key.ValidateIdentifier();
    }

    internal override void ValidateKeys(List<UserDTO>? users)
    {
        if (Guards.IsNotNullOrEmpty(users))
        {
            for (int i = 0; i < users.Count; i++)
            {
                users[i].DTOValidation();
            }
        }
    }

    public override void DTOValidation()
    {
        ValidateKey();
        Groups.ValidateKeys<GroupDTO, Group>();
        Roles.ValidateKeys<GroupRolesDTO, GroupRoles>();
    }
}
using ChatService.Api.DTOS.Users;
using ChatService.Api.Utils;
using ChatService.Core.Helpers;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.Users;

namespace ChatService.Api.DTOS.Groups;

public sealed class GroupDTO : BaseDTO<GroupDTO, Group>
{
    public GroupDTO(GroupKeyDTO key, UserDTO founder)
    {
        Key = key;
        Founder = founder;
    }

    public GroupKeyDTO Key { get; set; }
    public UserDTO Founder { get; set; }
    public List<UserDTO>? Users { get; set; }

    internal override void ValidateKey()
    {
        Key.ValidateIdentifier();
    }

    internal override void ValidateKeys(List<GroupDTO>? groups)
    {
        if (Guards.IsNotNullOrEmpty(groups))
        {
            for (int i = 0; i < groups.Count; i++)
            {
                groups[i].DTOValidation();
            }
        }
    }

    public override void DTOValidation()
    {
        ValidateKey();
        Founder.ValidateKey();
        Users.ValidateKeys<UserDTO, User>();
    }
}

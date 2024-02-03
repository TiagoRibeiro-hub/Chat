using ChatService.Api.DTOS.Users;
using ChatService.Core.Helpers;
using ChatService.Domain.Entities.Groups;

namespace ChatService.Api.DTOS.Groups;

public sealed class GroupDTO : BaseDTO<GroupDTO, Group>
{
    public GroupDTO(GroupKeyDTO key)
    {
        Key = key;
        IsPrivate = false;
    }

    public GroupKeyDTO Key { get; set; }
    public bool IsPrivate { get; set; }
    public Guid? Founder { get; set; }
    public List<UserKeyDTO>? Users { get; set; }

    internal override void ValidateKey()
    {
        Key.ValidateIdentifier();
    }

    internal override void ValidateKeys(List<GroupDTO>? groups)
    {
        if (Guards.IsNotNullOrEmptyCollection(groups))
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
        UserKeyDTO.ValidateKeys(Users);
    }
}

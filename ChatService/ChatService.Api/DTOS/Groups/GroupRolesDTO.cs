using ChatService.Api.DTOS.Users;
using ChatService.Core.Helpers;
using ChatService.Domain.Entities.Groups;

namespace ChatService.Api.DTOS.Groups;
public sealed class GroupRolesDTO : BaseDTO<GroupRolesDTO, GroupRoles>
{
    public GroupRolesDTO(GroupRolesTypesDTO groupRolesType, UserKeyDTO userKey)
    {
        GroupRolesType = groupRolesType;
        UserKey = userKey;
    }

    public GroupRolesTypesDTO GroupRolesType { get; set; }
    public UserKeyDTO UserKey { get; set; }
    public GroupKeyDTO? Group { get; set; }

    internal override void ValidateKey()
    {
        if (!Guards.IsNull(this.Group))
        {
            Group.ValidateIdentifier();
        }
        UserKey.ValidateIdentifier();
    }

    internal override void ValidateKeys(List<GroupRolesDTO>? roles)
    {
        if (Guards.IsNotNullOrEmptyCollection(roles))
        {
            for (int i = 0; i < roles.Count; i++)
            {
                roles[i].DTOValidation();
            }
        }
    }

    public override void DTOValidation()
    {
        ValidateKey();
    }
}

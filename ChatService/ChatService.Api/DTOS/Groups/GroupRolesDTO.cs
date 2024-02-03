using ChatService.Core.Helpers;
using ChatService.Domain.Entities.Groups;

namespace ChatService.Api.DTOS.Groups;
public sealed class GroupRolesDTO : BaseDTO<GroupRolesDTO, GroupRoles>
{
    public GroupRolesDTO(GroupRolesTypesDTO groupRolesType, GroupKeyDTO group)
    {
        GroupRolesType = groupRolesType;
        Group = group;
    }

    public GroupRolesTypesDTO GroupRolesType { get; set; }
    public GroupKeyDTO Group { get; set; }

    internal override void ValidateKey()
    {
        if (!Guards.IsNull(this.Group))
        {
            Group.ValidateIdentifier();
        }
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

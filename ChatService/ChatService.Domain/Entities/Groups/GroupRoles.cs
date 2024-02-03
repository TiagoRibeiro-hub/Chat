using System.Text.Json.Serialization;

namespace ChatService.Domain.Entities.Groups;

public sealed class GroupRoles
{
    public GroupRoles(GroupRolesTypes groupRolesType, GroupKey group)
    {
        GroupRolesType = groupRolesType;
        Group = group;
    }

    public GroupKey Group { get; set; }
    public GroupRolesTypes GroupRolesType { get; set; }
}
namespace ChatService.Domain.Entities.Groups;

public sealed class GroupRoles
{
    public GroupRoles(
        GroupRolesTypes groupRolesType,
        GroupKey group,
        UserKey user
        )
    {
        GroupRolesType = groupRolesType;
        Group = group;
        User = user;
    }

    public GroupKey Group { get; set; }
    public UserKey User { get; set; }
    public GroupRolesTypes GroupRolesType { get; set; }
}
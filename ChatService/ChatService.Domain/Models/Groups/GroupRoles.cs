using System.Text.Json.Serialization;

namespace ChatService.Domain.Models.Groups;

public sealed class GroupRoles
{
    public GroupRoles(GroupRolesTypes groupRolesType, UserKey userKey)
    {
        GroupRolesType = groupRolesType;
        UserKey = userKey;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GroupKey? Group { get; set; }
    public GroupRolesTypes GroupRolesType { get; set; }
    public UserKey UserKey { get; set; }
}
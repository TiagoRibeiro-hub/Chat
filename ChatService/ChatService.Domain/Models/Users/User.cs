using ChatService.Domain.Models.Groups;

namespace ChatService.Domain.Models.Users;

public sealed class User : Base<UserKey>
{
    public User(UserKey key)
    {
        Key = key;
    }

    public override UserKey Key { get; set; }
    public List<Group>? Groups { get; set; }
    public List<GroupRoles>? Roles { get; set; }
}

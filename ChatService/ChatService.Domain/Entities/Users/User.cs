using ChatService.Domain.Entities.Groups;

namespace ChatService.Domain.Entities.Users;

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

using ChatService.Domain.Models.Users;

namespace ChatService.Domain.Models.Groups;

public sealed class Group : Base<GroupKey>
{
    public Group(GroupKey key, User founder, bool @private)
    {
        Key = key;
        Founder = founder;
        IsPrivate = @private;
    }

    public override GroupKey Key { get; set; }
    public bool IsPrivate { get; set; }
    public User Founder { get; set; }
    public List<User>? Users { get; set; }
}

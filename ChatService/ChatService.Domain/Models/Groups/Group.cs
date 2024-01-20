using ChatService.Domain.Models.Users;

namespace ChatService.Domain.Models.Groups;

public sealed class Group : Base<GroupKey>
{
    public Group(GroupKey key, User founder)
    {
        Key = key;
        Founder = founder;
    }

    public override GroupKey Key { get; set; }
    public User Founder { get; set; }
    public List<User>? Users { get; set; }
}

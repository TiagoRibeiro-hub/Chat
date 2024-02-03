namespace ChatService.Domain.Entities.Groups;

public sealed class Group : Base<GroupKey>
{
    public Group(GroupKey key)
    {
        Key = key;
    }

    public override GroupKey Key { get; set; }
    public bool IsPrivate { get; set; }
    public Guid? Founder { get; set; }
    public List<UserKey>? Users { get; set; }
}

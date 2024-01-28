namespace ChatService.Domain.Entities;

public abstract class Base<T> where T : Key
{
    public abstract T Key { get; set; }
}

public abstract record BaseMessage();
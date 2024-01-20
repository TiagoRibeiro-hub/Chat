namespace ChatService.Domain.Models;

public abstract class Base<T> where T : Key
{
    public abstract T Key { get; set; }
}
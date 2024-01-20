using ChatService.Domain.Models;

namespace ChatService.Core.Services;

public interface IBaseService<T, K> where T : Base<K> where K : Key
{
    Task<T> Create(T item);
    Task<bool> Delete(K key);
    Task<T> Get(K key);
    Task<string> GetName(K key);
    Task<List<T>> List(bool complete);
}


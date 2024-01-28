using ChatService.Domain.Models;

namespace ChatService.Infrastructure.Data.Abstractions;
public interface IBaseService<T, K> where T : Base<K> where K : Key
{
    Task<T> CreateAsync(T item);
    Task<bool> DeleteAsync(K key);
    Task<T?> GetAsync(K key);
    Task<string?> GetNameAsync(K key);
    Task<List<T>> ListAsync(bool complete);
    Task<bool> UpdateAsync(T item);
}


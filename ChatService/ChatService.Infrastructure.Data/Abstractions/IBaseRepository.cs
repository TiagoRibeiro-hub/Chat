using ChatService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ItemKey = ChatService.Domain.Models.Key;

namespace ChatService.Infrastructure.Data.Abstractions;
public interface IBaseRepository<TContext> where TContext : DbContext, new()
{
    IUnitOfWork<TContext> UnitOfWork { get; }

    Task<T> CreateAsync<T, K>(T item) where T : Base<K> where K : ItemKey;
    Task<bool> DeleteAsync<T, K>(K key) where T : Base<K> where K : ItemKey;
    Task<T?> GetAsync<T, K>(K key) where T : Base<K> where K : ItemKey;
    Task<List<T>> ListAsync<T, K>(bool complete) where T : Base<K> where K : ItemKey;
    Task<bool> Update<T, K>(T item) where T : Base<K> where K : ItemKey;
}
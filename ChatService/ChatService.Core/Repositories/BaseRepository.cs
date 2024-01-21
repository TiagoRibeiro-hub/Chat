using ChatService.Domain.Models;
using ChatService.Infrastructure.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using ItemKey = ChatService.Domain.Models.Key;

namespace ChatService.Core.Repositories;

public sealed class BaseRepository<TContext> : IBaseRepository<TContext>
    where TContext : DbContext, new()
{

    private readonly IUnitOfWork<TContext> _unitOfWork;

    public BaseRepository(IUnitOfWork<TContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<T> CreateAsync<T, K>(T item) where T : Base<K> where K : ItemKey
    {
        try
        {
            if (item == null)
            {
                throw new Exception("Entity");
            }
            var entityTracker = await _unitOfWork.Context.Set<T>().AddAsync(item);
            if (entityTracker.State != EntityState.Added)
            {
                throw new Exception();
            }

            _unitOfWork.Save();
            return entityTracker.Entity;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public async Task<bool> DeleteAsync<T, K>(K key) where T : Base<K> where K : ItemKey
    {
        try
        {
            var entityTracker = _unitOfWork.Context.Set<T>().Remove(await GetAsync<T, K>(key));
            if (entityTracker.State != EntityState.Deleted)
            {
                throw new Exception();
            }

            _unitOfWork.Save();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<T> GetAsync<T, K>(K key) where T : Base<K> where K : ItemKey
    {
        var entity = await _unitOfWork.Context.Set<T>().Where(x => x.Key.Identifier == key.Identifier)
                            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new Exception();
        }

        return entity;
    }

    public async Task<List<T>> ListAsync<T, K>(bool complete) where T : Base<K> where K : ItemKey
    {
        List<T> list = new();
        if (complete)
        {
            throw new NotImplementedException();
        }
        else
        {
            list = await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        return list;
    }

    public Task<bool> Update<T, K>(T item) where T : Base<K> where K : ItemKey
    {
        try
        {
            var entityTracker = _unitOfWork.Context.Set<T>().Update(item);
            if (entityTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }

            _unitOfWork.Save();
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }
}
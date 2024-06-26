﻿using ChatService.Core.Abstractions;
using ChatService.Core.Helpers;
using ChatService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ItemKey = ChatService.Domain.Entities.Key;

namespace ChatService.Core.Repositories;

public sealed class BaseRepository<TContext> : IBaseRepository<TContext>
    where TContext : DbContext, new()
{
    public IUnitOfWork<TContext> UnitOfWork { get; }

    public BaseRepository(IUnitOfWork<TContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<T> CreateAsync<T, K>(T item) where T : Base<K> where K : ItemKey
    {
        try
        {
            if (item == null)
            {
                throw new Exception("Entity");
            }
            var entityTracker = await UnitOfWork.Context.Set<T>().AddAsync(item);
            if (entityTracker.State != EntityState.Added)
            {
                throw new Exception();
            }

            UnitOfWork.SaveChanges();
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
            var entity = await GetAsync<T, K>(key);
            if (Guards.IsNull(entity))
            {
                throw new Exception();
            }

            var entityTracker = UnitOfWork.Context.Set<T>().Remove(entity);
            if (entityTracker.State != EntityState.Deleted)
            {
                throw new Exception();
            }

            UnitOfWork.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<T?> GetAsync<T, K>(K key) where T : Base<K> where K : ItemKey
    {
        var entity = await UnitOfWork.Context.Set<T>().Where(x => x.Key.Identifier == key.Identifier)
                            .FirstOrDefaultAsync();
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
            list = await UnitOfWork.Context.Set<T>().ToListAsync();
        }

        return list;
    }

    public Task<bool> Update<T, K>(T item) where T : Base<K> where K : ItemKey
    {
        try
        {
            var entityTracker = UnitOfWork.Context.Set<T>().Update(item);
            if (entityTracker.State != EntityState.Modified)
            {
                throw new Exception();
            }

            UnitOfWork.SaveChanges();
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }
}
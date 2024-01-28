﻿using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace ChatService.Infrastructure.Data.Abstractions;

public interface IUnitOfWork<out TContext> where TContext : DbContext, new()
{
    TContext Context { get; }
    void CreateTransaction();
    void Dispose();
    void Commit();
    void Rollback();
    void Save();
}
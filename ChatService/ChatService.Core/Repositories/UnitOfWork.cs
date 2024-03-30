using ChatService.Core.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace ChatService.Core.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork<TContext>
    where TContext : DbContext, new()
{
    private IDbContextTransaction? _objTran { get; set; }
    public TContext Context { get; }

    public UnitOfWork()
    {
        Context = new TContext();
    }

    public void CreateTransaction()
    {
        _objTran = Context.Database.BeginTransaction();
    }

    public void Dispose()
    {
        IsTransactionNotNull(_objTran);
        _objTran.Dispose();
    }

    public void Commit()
    {
        IsTransactionNotNull(_objTran);
        _objTran.Commit();
    }

    public void Rollback()
    {
        IsTransactionNotNull(_objTran);
        _objTran.Rollback();
        _objTran.Dispose();
    }

    public void Save()
    {
        try
        {
            Context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("SaveChanges", ex);
        }
    }

    #region
    private void IsTransactionNotNull([NotNull] IDbContextTransaction? objTran)
    {
        if (objTran == null)
        {
            throw new Exception("No Transaction");
        }
    }
    #endregion

}
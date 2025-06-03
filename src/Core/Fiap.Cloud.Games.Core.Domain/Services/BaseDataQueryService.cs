using Fiap.Cloud.Games.Core.Domain.Entities;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Cloud.Games.Core.Domain.Services;

public abstract class BaseDataQueryService : IDisposable
{
    protected readonly IApplicationDBContext _context;

    protected BaseDataQueryService(IApplicationDBContext applicationDBContext)
        => _context = applicationDBContext;
    
    protected virtual IQueryable<TEntity> BaseQuery<TEntity>(bool tracking) where TEntity : Identifier
    {
        var query = _context.Set<TEntity>().AsQueryable();

        if (tracking)
            query = query.AsTracking();
        else
            query = query.AsNoTracking();

        return query;
    }

    public virtual IQueryable<TEntity> ById<TEntity>(Guid id, bool tracking) where TEntity : Identifier
        => BaseQuery<TEntity>(tracking).Where(x => x.Id == id);

    public virtual void Dispose()
    {
        _context?.Dispose();

        GC.SuppressFinalize(this);
    }
}

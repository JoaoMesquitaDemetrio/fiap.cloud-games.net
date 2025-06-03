using Fiap.Cloud.Games.Core.Domain.Entities;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Sample.Utils.Extensions;

namespace Fiap.Cloud.Games.Core.Domain.Services;

public abstract class BaseService : BaseDataQueryService
{
    public BaseService(IApplicationDBContext applicationDBContext) : base(applicationDBContext) { }

    public void BaseDetach<TEntity>(TEntity entity) where TEntity : Identifier
    {
        if (!entity.IsNull())
            _context.Entry(entity).State = EntityState.Detached;
    }
}

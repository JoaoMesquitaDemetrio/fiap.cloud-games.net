using Fiap.Cloud.Games.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;

public interface IApplicationDBContext : IDisposable
{
    DatabaseFacade Database { get; }
    DbSet<T> Set<T>() where T : class;
    EntityEntry<T> Entry<T>(T entity) where T : class;
    EntityEntry<T> Attach<T>(T entity) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Repository;

namespace Fiap.Cloud.Games.Core.Application;

public abstract class BaseApplicationService : IDisposable
{
    protected readonly IApplicationDBContext _context;

    protected BaseApplicationService(IApplicationDBContext applicationDBContext)
        => _context = applicationDBContext;

    protected async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
    
    public virtual void Dispose()
    {
        _context?.Dispose();

        GC.SuppressFinalize(this);
    }
}

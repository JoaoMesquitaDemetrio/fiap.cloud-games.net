using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fiap.Cloud.Games.Core.Domain.Extensions;

public static class AppExtensions
{
    public static async Task<EntityEntry<T>> UpdateAsync<T>(this DbSet<T> data, T entity) where T : class
        => await Task.Run(() => data.Update(entity));
}

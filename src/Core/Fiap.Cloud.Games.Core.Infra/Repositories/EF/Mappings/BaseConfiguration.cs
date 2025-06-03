
using Fiap.Cloud.Games.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Cloud.Games.Core.Infra.Repositories.EF.Mappings;

public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : Identifier
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(typeof(T).Name);
        builder.Property(x => x.Id).ValueGeneratedNever();
    }
}

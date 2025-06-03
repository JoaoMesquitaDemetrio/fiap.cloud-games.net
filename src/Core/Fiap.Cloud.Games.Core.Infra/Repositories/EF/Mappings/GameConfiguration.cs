
using Fiap.Cloud.Games.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Cloud.Games.Core.Infra.Repositories.EF.Mappings;

public class GameConfiguration : BaseConfiguration<Game>
{
    public override void Configure(EntityTypeBuilder<Game> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Studio).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Price).IsRequired().HasColumnType("numeric(18,2)");
        builder.Property(x => x.AgeRating).IsRequired();
    }
}


using Fiap.Cloud.Games.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Cloud.Games.Core.Infra.Repositories.EF.Mappings;

public class PlayerConfiguration : BaseConfiguration<Player>
{
    public override void Configure(EntityTypeBuilder<Player> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Type).IsRequired().HasColumnType("int");
    }
}

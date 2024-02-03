using ChatService.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Key.Identifier).HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        builder.Property(x => x.Key.Name).HasColumnType("nvarchar(150)")
            .IsRequired();

        builder.OwnsMany(x => x.Groups, j =>
        {
            j.ToJson();
        });        
        
        builder.OwnsMany(x => x.Roles, j =>
        {
            j.ToJson();
        });

        // Keys
        builder.HasKey(x => x.Key.Identifier);

        // Indexes
        builder.HasIndex(x => x.Key.Identifier).IsUnique();
        builder.HasIndex(x => x.Key.Name);

        // Table Name
        builder.ToTable(Domain.Constants.Table.Users);
    }
}
using ChatService.Domain.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.Key.Identifier)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("group_id")
            .IsRequired()
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        builder.Property(x => x.Key.Name).HasColumnType("nvarchar(150)")
            .HasColumnName("group_name")
            .IsRequired();

        builder.Property(x => x.Founder).HasColumnType("uniqueidentifier").IsRequired();
        
        builder.Property(x => x.IsPrivate).HasColumnType("bit").IsRequired();

        builder.OwnsMany(x => x.Users, j =>
        {
            j.ToJson();
        });

        // Keys
        builder.HasKey(x => x.Key.Identifier);

        // Indexes
        builder.HasIndex(x => x.Key.Identifier).IsUnique();
        builder.HasIndex(x => x.IsPrivate);

        // Table Name
        builder.ToTable(Domain.Constants.Table.Groups);
    }
}


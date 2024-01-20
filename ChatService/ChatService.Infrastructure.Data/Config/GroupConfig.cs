using ChatService.Domain.Models.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.Key.Identifier).ValueGeneratedOnAdd().HasColumnType("uniqueidentifier").HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        builder.Property(x => x.Key.Name).HasColumnType("nvarchar(150)").IsRequired();
        builder.Property(x => x.Users).HasJsonPropertyName("Users");

        // Relations
        builder.HasOne(x => x.Founder).WithMany(x => x.Groups).HasForeignKey(x => x.Key.Identifier).OnDelete(DeleteBehavior.Cascade);

        // Keys and Indexes
        builder.HasIndex(x => x.Key.Identifier);
        builder.HasIndex(x => x.Key.Identifier).IsUnique();
        builder.HasIndex(x => x.Key.Name);

        // Table Name
        builder.ToTable(Domain.Constants.Table.Groups);
    }
}


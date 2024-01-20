using ChatService.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Key.Identifier).ValueGeneratedOnAdd().HasColumnType("uniqueidentifier").HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        builder.Property(x => x.Key.Name).HasColumnType("nvarchar(150)").IsRequired();
        builder.Property(x => x.Groups).HasJsonPropertyName("Groups");
        builder.Property(x => x.Roles).HasJsonPropertyName("Roles");

        // Keys and Indexes
        builder.HasKey(x => x.Key.Identifier);
        builder.HasIndex(x => x.Key.Identifier).IsUnique();
        builder.HasIndex(x => x.Key.Name);

        // Table Name
        builder.ToTable(Domain.Constants.Table.Users);
    }
}
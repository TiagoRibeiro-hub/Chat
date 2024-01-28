using ChatService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class MessageConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(x => x.Identifier).HasColumnType("uniqueidentifier")
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.Text).HasColumnType("nvarchar(max)")
            .HasColumnName("message")
            .IsRequired();

        builder.Property(x => x.Date).HasJsonPropertyName("datetime2").IsRequired();

        // Keys
        builder.HasKey(x => x.Identifier);

        // Indexes
        builder.HasIndex(x => x.Identifier).IsUnique();
        builder.HasIndex(x => x.Date);

        // Table Name
        builder.ToTable(Domain.Constants.Table.UserGroups);
    }
}

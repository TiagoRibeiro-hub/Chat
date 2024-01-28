using ChatService.Domain.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class UserMessageConfig : IEntityTypeConfiguration<UserMessage>
{
    public void Configure(EntityTypeBuilder<UserMessage> builder)
    {
        builder.Property(x => x.UserIdentifier).HasColumnType("uniqueidentifier")
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(x => x.Text).HasColumnType("nvarchar(max)")
            .HasColumnName("message")
            .IsRequired();

        builder.Property(x => x.Date).HasJsonPropertyName("datetime2").IsRequired();

        // Keys
        builder.HasKey(x => x.UserIdentifier);

        // Indexes
        builder.HasIndex(x => x.UserIdentifier).IsUnique();
        builder.HasIndex(x => x.Date);

        // Table Name
        builder.ToTable(Domain.Constants.Table.UserGroups);
    }
}

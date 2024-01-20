using ChatService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class MessageConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(x => x.Identifier).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Text).HasColumnType("nvarchar(450)").IsRequired();
        builder.Property(x => x.Date).HasJsonPropertyName("datetime2").IsRequired();

        // Keys and Indexes
        builder.HasKey(x => x.Identifier);
        builder.HasIndex(x => x.Identifier).IsUnique();

        // Table Name
        builder.ToTable(Domain.Constants.Table.UserGroups);
    }
}

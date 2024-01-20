using ChatService.Domain.Models.GroupUserMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class GroupUserMessagesConfig : IEntityTypeConfiguration<GroupUserMessages>
{
    public void Configure(EntityTypeBuilder<GroupUserMessages> builder)
    {
        builder.Property(x => x.Group.Key.Identifier).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Users).HasJsonPropertyName("User").IsRequired();
        builder.Property(x => x.Messages).HasJsonPropertyName("Messages").IsRequired();

        // Relations

        // Keys and Indexes
        builder.HasIndex(x => x.Group.Key.Identifier);
        builder.HasIndex(x => x.Group.Key.Identifier).IsUnique();
        builder.HasIndex(x => x.Group.Key.Name);

        // Table Name
        builder.ToTable(Domain.Constants.Table.UserGroups);
    }
}


using ChatService.Domain.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Data.Config;

public sealed class GroupMessagesConfig : IEntityTypeConfiguration<GroupMessages>
{
    public void Configure(EntityTypeBuilder<GroupMessages> builder)
    {
        builder.Property(x => x.GroupIdentifier).HasColumnType("uniqueidentifier").IsRequired();

        builder.OwnsMany(x => x.Messages, j =>
        {
            j.ToJson();
        });

        // Keys
        builder.HasIndex(x => x.GroupIdentifier);

        // Indexes
        builder.HasIndex(x => x.GroupIdentifier).IsUnique();

        // Table Name
        builder.ToTable(Domain.Constants.Table.UserGroups);
    }
}


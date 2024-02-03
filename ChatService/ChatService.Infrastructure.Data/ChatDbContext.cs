using ChatService.Domain.Entities.Groups;
using ChatService.Domain.Entities.Messages;
using ChatService.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ChatService.Infrastructure.Data;
public class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
    : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, (_, level) => level == LogLevel.Information)
            .EnableSensitiveDataLogging()
            .UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupMessages> GroupMessages { get; set; }
    public DbSet<UserMessage> Messages { get; set; }
}

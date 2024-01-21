using ChatService.Domain.Models;
using ChatService.Domain.Models.Groups;
using ChatService.Domain.Models.GroupUserMessages;
using ChatService.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupUserMessages> GroupUserMessages { get; set; }
    public DbSet<Message> Messages { get; set; }
}

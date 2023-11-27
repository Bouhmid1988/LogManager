using LogManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LogManager.Data;

/// <summary>
/// Represents the database context for Log Manager related entities.
/// </summary>
public sealed class LogDbContext : DbContext
{
    public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
    {
        Database.EnsureCreated(); 

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    public DbSet<LogMessage> LoggerMessage { get; set; }
    public DbSet<Application> Application { get; set; }
}
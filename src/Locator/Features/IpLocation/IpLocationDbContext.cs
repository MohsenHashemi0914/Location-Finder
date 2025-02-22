using Locator.Features.IpLocation.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Locator.Features.IpLocation;

public sealed class IpLocationDbContext(DbContextOptions<IpLocationDbContext> options) : DbContext(options)
{
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Location>().ToCollection(nameof(Locations));
    }
}
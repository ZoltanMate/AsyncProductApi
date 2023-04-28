using AsyncProductApi.Domain.Product;
using AsyncProductApi.Domain.RequestInfo;
using Microsoft.EntityFrameworkCore;

namespace AsyncProductApi.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
            
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<RequestInfo> RequestInfos => Set<RequestInfo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<RequestInfo>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<RequestInfo>()
            .HasMany<Product>()
            .WithOne()
            .HasForeignKey(x => x.RequestInfoId)
            .IsRequired();
    }
}
using CampoSystem.ErpAI.Domain.Products;
using CampoSystem.ErpAI.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CampoSystem.ErpAI.Infrastructure.Persistence;

public sealed class ProductDbContext : DbContext
{

    public ProductDbContext(DbContextOptions<ProductDbContext> options) 
        : base(options)
    {

    }

    public DbSet<Product> Products
    {
        get; set;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
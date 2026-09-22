using CampoSystem.ErpAI.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CampoSystem.ErpAI.Infrastructure.Persistence;

public class ProductDbContext : DbContext
{

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {

    }
    DbSet<Product> Products
    {
        get; set;
    }
}
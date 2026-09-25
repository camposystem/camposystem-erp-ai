using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Domain.Products;

namespace CampoSystem.ErpAI.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{

    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public Task<Product> AddAsync(Product product)
    {
        _dbContext.Products.Add(product);

        _dbContext.SaveChanges();

        return Task.FromResult(product);
    }
}

using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CampoSystem.ErpAI.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{

    private readonly ProductDbContext _dbContext;

    public ProductRepository(ProductDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public async Task<Product> AddAsync(Product product)
    {
        _dbContext.Products.Add(product);

        await _dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
       return await _dbContext.Products.ToListAsync();
    }
}

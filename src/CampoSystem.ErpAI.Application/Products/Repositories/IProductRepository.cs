using CampoSystem.ErpAI.Domain.Products;

namespace CampoSystem.ErpAI.Application.Products.Repositories;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);
}

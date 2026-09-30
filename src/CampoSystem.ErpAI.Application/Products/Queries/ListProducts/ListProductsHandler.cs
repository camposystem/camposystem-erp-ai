using CampoSystem.ErpAI.Application.Products.Repositories;

namespace CampoSystem.ErpAI.Application.Products.Queries.ListProducts;

public sealed class ListProductsHandler
{
    private readonly IProductRepository _repository;

    public ListProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }
}
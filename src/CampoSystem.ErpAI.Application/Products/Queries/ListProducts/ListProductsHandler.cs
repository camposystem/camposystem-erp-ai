using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Application.Products.Response;

namespace CampoSystem.ErpAI.Application.Products.Queries.ListProducts;

public sealed class ListProductsHandler
{
    private readonly IProductRepository _repository;

    public ListProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ListProductsResponse>> Handle(ListProductsQuery query)
    {
        var products = await _repository.GetAllAsync();

        return products
            .Select(p => new ListProductsResponse(
                    Id: p.Id,
                    Name: p.Name.Name,
                    Sku: p.Sku.Sku,
                    Price: p.Price?.Amount,
                    Description: p.Description))
            .ToList();
    }
}
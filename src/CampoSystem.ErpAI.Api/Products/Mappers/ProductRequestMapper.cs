using CampoSystem.ErpAI.Api.Products.Requests;
using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;

namespace CampoSystem.ErpAI.Api.Products.Mappers;

public static class ProductRequestMapper
{

    public static CreateProductCommand ToCreateProductCommand(CreateProductRequest request)
    {
        return new CreateProductCommand(
            request.Name,
            request.Sku,
            request.Price,
            request.Description);
    }
}

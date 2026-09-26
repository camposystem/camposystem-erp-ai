using CampoSystem.ErpAI.Api.Products.Mappers;
using CampoSystem.ErpAI.Api.Products.Requests;
using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;
using CampoSystem.ErpAI.Application.Response;
using CampoSystem.ErpAI.SharedKernel.Common.Result;

namespace CampoSystem.ErpAI.Api.Products.Endpoints;

public static class ProductsEndpoints
{
    public static void MapCreateProduct(WebApplication app)
    {
        app.MapPost("/api/products", async (CreateProductRequest request, CreateProductCommandHandler handler) =>
        {
            var command = ProductRequestMapper.ToCreateProductCommand(request);
            var result = await handler.Handle(command);

            if (result.IsSuccess)
            {
                return Results.Created("", (result.Value));
            }

            return Results.BadRequest(result.Errors);
        }).Produces<CreateProductResponse>(201)
        .Produces<IReadOnlyList<Error>>(400);
    }
}

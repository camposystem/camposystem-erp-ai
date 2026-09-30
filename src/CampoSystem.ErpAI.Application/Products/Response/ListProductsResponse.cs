namespace CampoSystem.ErpAI.Application.Products.Response;

public sealed record ListProductsResponse(
    Guid Id,
    string Name,
    string Sku,
    decimal? Price,
    string Description);

namespace CampoSystem.ErpAI.Api.Products.Requests;


public sealed record CreateProductRequest(
    string Name,
    string Sku,
    decimal? Price,
    string Description = "");

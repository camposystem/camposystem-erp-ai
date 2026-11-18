using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;
using CampoSystem.ErpAI.Application.Products.Queries.ListProducts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CampoSystem.ErpAI.Application.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddScoped<CreateProductCommandHandler>()
            .AddScoped<ListProductsHandler>();

        services.AddValidatorsFromAssembly(
            typeof(CreateProductCommandValidator).Assembly);

        return services;
    }
}
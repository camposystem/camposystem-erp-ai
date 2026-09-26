using CampoSystem.ErpAI.Api.Products.Endpoints;
using CampoSystem.ErpAI.Application.Products.Commands.CreateProduct;
using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CreateProductCommandHandler>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "CampoSystem.ErpAI.Api v1"));
app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

ProductsEndpoints.MapCreateProduct(app);

app.Run();

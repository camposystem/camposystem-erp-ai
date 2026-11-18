using CampoSystem.ErpAI.Api.IoC;
using CampoSystem.ErpAI.Api.Products.Endpoints;
using CampoSystem.ErpAI.Application.IoC;
using CampoSystem.ErpAI.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApi(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}

app.UseSwaggerUI(o =>
    o.SwaggerEndpoint("https://localhost:7141/openapi/v1.json", "CampoSystem.ErpAI.Api v1"));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
ProductsEndpoints.MapCreateProduct(app);
ProductsEndpoints.MapListProducts(app);

app.Run();

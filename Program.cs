using Wheem.Data;
using Wheem.Models;
using Wheem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger/OpenAPI -> https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WheemContext>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/products", async (IProductService service) => await service.GetAll())
	.WithName("GetProducts")
	.WithOpenApi();

app.MapGet("/products/{id}", async (IProductService service, int id) => await service.GetOne(id))
	.WithName("GetProduct")
	.WithOpenApi();

app.MapPost("/products", async (IProductService service, Product product) => await service.Create(product))
	.WithName("AddProduct")
	.WithOpenApi();

app.MapPut("/products/{id}", async (IProductService service, int id, Product product) => await service.Update(id, product))
	.WithName("UpdateProduct")
	.WithOpenApi();

app.MapDelete("/products/{id}", async (IProductService service, int id) => await service.Delete(id))
	.WithName("DeleteProduct")
	.WithOpenApi();

app.Run();

using Wheem.Models;
using Wheem.Services;

namespace Microsoft.AspNetCore.Routing
{
	public static class ProductApi
	{
		public static RouteGroupBuilder MapProductApi(this RouteGroupBuilder builder)
		{
			builder.MapGet("/", async (IProductService service)
				=> await service.GetAll());

			builder.MapGet("/{id}", async (int id, IProductService service)
				=> await service.GetOne(id));

			builder.MapPost("/", async (Product product, IProductService service)
				=> await service.Create(product));

			builder.MapPut("/{id}", async (int id, Product product, IProductService service)
				=> await service.Update(id, product));

			builder.MapDelete("/{id}", async (int id, IProductService service)
				=> await service.Delete(id));

			return builder;
		}
	}
}

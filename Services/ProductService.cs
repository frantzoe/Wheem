using Microsoft.EntityFrameworkCore;
using Weesh.Utils;
using Wheem.Data;
using Wheem.Models;

namespace Wheem.Services
{
	public class ProductService(WheemContext wheemContext) : IProductService
	{
		private readonly WheemContext context = wheemContext;

		/**
		 * <summary>Get all products.</summary>
		 * <returns>A list of all products.</returns>
		**/
		public async Task<IResult> GetAll()
		{
			return Results.Ok(await context.Products.ToListAsync());
		}

		/**
		 * <summary>Get one product.</summary>
		 * <param name="id">The id of the product to get.</param>
		 * <returns>The product with the given id.</returns>
		**/
		public async Task<IResult> GetOne(int id)
		{
			var existingProduct = await context.Products.FindAsync(id);

			return existingProduct is null
				? Results.NotFound($"Product with id {id} does not exist.")
				: Results.Ok(existingProduct);
		}

		/**
		 * <summary>Create an product.</summary>
		 * <param name="product">The product to create.</param>
		 * <returns>The created product.</returns>
		**/
		public async Task<IResult> Create(Product product)
		{
			context.Products.Add(await ProductUtils.SaveImage(product));
			await context.SaveChangesAsync();
			return Results.Created($"/products/{product.Id}", product);
		}

		/**
		 * <summary>Update an product.</summary>
		 * <param name="id">The id of the product to update.</param>
		 * <param name="product">The product to update.</param>
		 * <returns>The updated product.</returns>
		**/
		public async Task<IResult> Update(int id, Product product)
		{
			if (id != product.Id)
			{
				return Results.BadRequest($"Product ids {id} and {product.Id} do not match.");
			}

			var existingProduct = context.Products.Find(id);

			if (existingProduct is null)
			{
				return Results.NotFound($"Product with id {id} does not exist.");
			}

			context.Entry(existingProduct).CurrentValues.SetValues(await ProductUtils.SaveImage(product));

			return Results.Ok(existingProduct);
		}

		/**
		 * <summary>Delete an product.</summary>
		 * <param name="id">The id of the product to delete.</param>
		 * <returns>The deleted product.</returns>
		**/
		public async Task<IResult> Delete(int id)
		{
			var existingProduct = context.Products.Find(id);

			if (existingProduct is null)
			{
				return Results.NotFound($"Product with id {id} does not exist.");
			}

			context.Products.Remove(existingProduct);
			await context.SaveChangesAsync();

			return Results.Ok();
		}

		/**
		 * <summary>Get the sum of all product prices.</summary>
		 * <returns>The sum of all product prices.</returns>
		**/
		public async Task<IResult> GetPriceSum()
		{
			return Results.Ok(await context.Products.SumAsync(product => product.Price));
		}
	}
}

using Wheem.Models;

namespace Wheem.Services
{
	public interface IProductService
	{
		/**
		 * <summary>Get all products.</summary>
		 * <returns>A list of all products.</returns>
		**/
		Task<IResult> GetAll();

		/**
		 * <summary>Get one product.</summary>
		 * <param name="id">The id of the product to get.</param>
		 * <returns>The product with the given id.</returns>
		**/
		Task<IResult> GetOne(int id);

		/**
		 * <summary>Create an product.</summary>
		 * <param name="product">The product to create.</param>
		 * <returns>The created product.</returns>
		**/
		Task<IResult> Create(Product product);

		/**
		 * <summary>Update an product.</summary>
		 * <param name="id">The id of the product to update.</param>
		 * <param name="product">The product to update.</param>
		 * <returns>The updated product.</returns>
		**/
		Task<IResult> Update(int id, Product product);

		/**
		 * <summary>Delete an product.</summary>
		 * <param name="id">The id of the product to delete.</param>
		 * <returns>The deleted product.</returns>
		**/
		Task<IResult> Delete(int id);

		/**
		 * <summary>Get the sum of all product prices.</summary>
		 * <returns>The sum of all product prices.</returns>
		**/
		Task<IResult> GetPriceSum();
	}
}

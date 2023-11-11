using Wheem.Models;

namespace Wheem.Services
{
	public interface IArticleService
	{
		/**
		 * <summary>Get all articles.</summary>
		 * <returns>A list of all articles.</returns>
		**/
		Task<IResult> GetAll();

		/**
		 * <summary>Get one article.</summary>
		 * <param name="id">The id of the article to get.</param>
		 * <returns>The article with the given id.</returns>
		**/
		Task<IResult> GetOne(int id);

		/**
		 * <summary>Create an article.</summary>
		 * <param name="article">The article to create.</param>
		 * <returns>The created article.</returns>
		**/
		Task<IResult> Create(Article article);

		/**
		 * <summary>Update an article.</summary>
		 * <param name="id">The id of the article to update.</param>
		 * <param name="article">The article to update.</param>
		 * <returns>The updated article.</returns>
		**/
		Task<IResult> Update(int id, Article article);

		/**
		 * <summary>Delete an article.</summary>
		 * <param name="id">The id of the article to delete.</param>
		 * <returns>The deleted article.</returns>
		**/
		Task<IResult> Delete(int id);
		
		/**
		 * <summary>Get the sum of all article prices.</summary>
		 * <returns>The sum of all article prices.</returns>
		**/
		Task<IResult> GetPriceSum();
	}
}

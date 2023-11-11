using Microsoft.EntityFrameworkCore;
using Weesh.Utils;
using Wheem.Data;
using Wheem.Models;

namespace Wheem.Services
{
	public class ArticleService(WheemContext wheemContext) : IArticleService
	{
		private readonly WheemContext context = wheemContext;

		/**
		 * <summary>Get all articles.</summary>
		 * <returns>A list of all articles.</returns>
		**/
		public async Task<IResult> GetAll()
		{
			return Results.Ok(await context.Articles.ToListAsync());
		}

		/**
		 * <summary>Get one article.</summary>
		 * <param name="id">The id of the article to get.</param>
		 * <returns>The article with the given id.</returns>
		**/
		public async Task<IResult> GetOne(int id)
		{
			var existingArticle = await context.Articles.FindAsync(id);

			return existingArticle is null
				? Results.NotFound($"Article with id {id} does not exist.")
				: Results.Ok(existingArticle);
		}

		/**
		 * <summary>Create an article.</summary>
		 * <param name="article">The article to create.</param>
		 * <returns>The created article.</returns>
		**/
		public async Task<IResult> Create(Article article)
		{
			context.Articles.Add(await ArticleUtils.SaveImage(article));
			await context.SaveChangesAsync();
			return Results.Created($"/articles/{article.Id}", article);
		}

		/**
		 * <summary>Update an article.</summary>
		 * <param name="id">The id of the article to update.</param>
		 * <param name="article">The article to update.</param>
		 * <returns>The updated article.</returns>
		**/
		public async Task<IResult> Update(int id, Article article)
		{
			if (id != article.Id)
			{
				return Results.BadRequest($"Article ids {id} and {article.Id} do not match.");
			}

			var existingArticle = context.Articles.Find(id);

			if (existingArticle is null)
			{
				return Results.NotFound($"Article with id {id} does not exist.");
			}

			context.Entry(existingArticle).CurrentValues.SetValues(await ArticleUtils.SaveImage(article));

			return Results.Ok(existingArticle);
		}

		/**
		 * <summary>Delete an article.</summary>
		 * <param name="id">The id of the article to delete.</param>
		 * <returns>The deleted article.</returns>
		**/
		public async Task<IResult> Delete(int id)
		{
			var existingArticle = context.Articles.Find(id);

			if (existingArticle is null)
			{
				return Results.NotFound($"Article with id {id} does not exist.");
			}

			context.Articles.Remove(existingArticle);
			await context.SaveChangesAsync();

			return Results.Ok();
		}

		/**
		 * <summary>Get the sum of all article prices.</summary>
		 * <returns>The sum of all article prices.</returns>
		**/
		public async Task<IResult> GetPriceSum()
		{
			return Results.Ok(await context.Articles.SumAsync(article => article.Price));
		}
	}
}

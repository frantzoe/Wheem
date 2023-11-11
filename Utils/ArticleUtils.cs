using Wheem.Models;

namespace Weesh.Utils
{
	public static class ArticleUtils
	{
		/**
		 * <summary>Save an image to the server.</summary>
		 * <param name="article">The article to save the image of.</param>
		 * <returns>The article with the image saved.</returns>
		**/
		public static async Task<Article> SaveImage(Article article)
		{
			string? imageUri = article.Image;
			if (imageUri is not null && Uri.IsWellFormedUriString(imageUri, UriKind.Absolute))
			{
				HttpClient httpClient = new();
				Stream fileStream = await httpClient.GetStreamAsync(imageUri);
				string articleImage = $"{article.Label.Replace(" ", "-").ToLower()}.jpg";
				await fileStream.CopyToAsync(
					new FileStream($"wwwroot/images/{articleImage}", FileMode.Create)
				);
				article.Image = articleImage;
			}
			return article;
		}
	}
}

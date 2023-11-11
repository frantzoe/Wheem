using Wheem.Models;

namespace Weesh.Utils
{
	public static class ProductUtils
	{
		/**
		 * <summary>Save an image to the server.</summary>
		 * <param name="product">The product to save the image of.</param>
		 * <returns>The product with the image saved.</returns>
		**/
		public static async Task<Product> SaveImage(Product product)
		{
			string? imageUri = product.Image;
			if (imageUri is not null && Uri.IsWellFormedUriString(imageUri, UriKind.Absolute))
			{
				HttpClient httpClient = new();
				Stream fileStream = await httpClient.GetStreamAsync(imageUri);
				string productImage = $"{product.Label.Replace(" ", "-").ToLower()}.jpg";
				await fileStream.CopyToAsync(
					new FileStream($"wwwroot/images/{productImage}", FileMode.Create)
				);
				product.Image = productImage;
			}
			return product;
		}
	}
}

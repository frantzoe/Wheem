/** This file contains the Article class, which represents an article on a website.
 * It contains the following properties:
 * - Id: the article's ID
 * - Label: the article's label
 * - Price: the article's price
 * - Link: the article's link
 * - Image: the article's image
 * It also contains a ToString() method, which returns a string representation of the article.
**/
namespace Wheem.Models
{
	public class Article(int Id, string Label, double Price, string Link, string? Image = null)
	{
		public int Id { get; set; } = Id;
		public string Label { get; set; } = Label;
		public double Price { get; set; } = Price;
		public string Link { get; set; } = Link;
		public string? Image { get; set; } = Image;
		public override string ToString() => $"Article {Id}: {Label} (${Price})";
	}
}

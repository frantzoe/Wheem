/** This file contains the Product class, which represents a product on a website.
 * It contains the following properties:
 * - Id: the product's ID
 * - Label: the product's label
 * - Price: the product's price
 * - Link: the product's link
 * - Image: the product's image
 * It also contains a ToString() method, which returns a string representation of the product.
**/
namespace Wheem.Models
{
	public class Product(int Id, string Label, double Price, string Link, string? Image = null)
	{
		public int Id { get; set; } = Id;
		public string Label { get; set; } = Label;
		public double Price { get; set; } = Price;
		public string Link { get; set; } = Link;
		public string? Image { get; set; } = Image;
		public override string ToString() => $"Product {Id}: {Label} (${Price})";
	}
}

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

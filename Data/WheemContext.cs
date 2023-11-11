using Microsoft.EntityFrameworkCore;
using Wheem.Models;

namespace Wheem.Data
{
	public class WheemContext : DbContext
	{
		public string DbPath { get; }
		public DbSet<Article> Articles { get; set; }

	public WheemContext()
	{
		DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "wheem.db");
	}

	// The following configures EF to create a Sqlite database file in the special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Article>().HasIndex(article => article.Link).IsUnique();
		}
	}
}

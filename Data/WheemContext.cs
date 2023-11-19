using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wheem.Models;

namespace Wheem.Data
{
	public class WheemContext : IdentityDbContext
	{
		public string DbPath { get; }
		public DbSet<Product> Products { get; set; }

		public WheemContext()
		{
			DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "wheem.db");
			Products = Set<Product>();
		}

		// The following configures EF to create a Sqlite database file in the special "home" folder for your platform.
		protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source=Wheem.db");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Product>().HasIndex(product => product.Link).IsUnique();
		}
	}
}

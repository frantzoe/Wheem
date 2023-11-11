using Microsoft.EntityFrameworkCore;
using Wheem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger/OpenAPI to the container -> https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

using var context = new WheemContext();

app.MapGet("/articles", async () => {
	return Results.Ok(await context.Articles.ToListAsync());
})
.WithName("GetArticles")
.WithOpenApi();

app.MapGet("/articles/{id}", async (int id) => {

	var existingArticle = await context.Articles.FindAsync(id);

	if (existingArticle is null)
	{
		return Results.NotFound($"Article with id {id} does not exist.");
	}

	return Results.Ok(existingArticle);
})
.WithName("GetArticle")
.WithOpenApi();

app.MapPost("/articles", async (Article article) => {
	context.Articles.Add(article);
	await context.SaveChangesAsync();
	return Results.Created($"/articles/{article.Id}", article);
})
.WithName("AddArticle")
.WithOpenApi();

app.MapPut("/articles/{id}", async (int id, Article article) => {
	if (id != article.Id)
	{
		return Results.BadRequest($"Article ids {id} and {article.Id} do not match.");
	}

	var existingArticle = await context.Articles.FindAsync(id);

	if (existingArticle is null)
	{
		return Results.NotFound($"Article with id {id} does not exist.");
	}

	context.Entry(existingArticle).CurrentValues.SetValues(article);

	return Results.Ok(await context.Articles.FindAsync(id));
})
.WithName("UpdateArticle")
.WithOpenApi();

app.MapDelete("/articles/{id}", async (int id) => {
	var article = await context.Articles.FindAsync(id);

	if (article is null)
	{
		return Results.NotFound($"Article with id {id} does not exist.");
	}

	context.Articles.Remove(article);
	await context.SaveChangesAsync();

	return Results.Ok(await context.Articles.FindAsync(id));
})
.WithName("DeleteArticle")
.WithOpenApi();

app.Run();

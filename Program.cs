using Wheem.Data;
using Wheem.Models;
using Wheem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger/OpenAPI -> https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WheemContext>();
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapGet("/articles", async (IArticleService service) => await service.GetAll())
	.WithName("GetArticles")
	.WithOpenApi();

app.MapGet("/articles/{id}", async (IArticleService service, int id) => await service.GetOne(id))
	.WithName("GetArticle")
	.WithOpenApi();

app.MapPost("/articles", async (IArticleService service, Article article) => await service.Create(article))
	.WithName("AddArticle")
	.WithOpenApi();

app.MapPut("/articles/{id}", async (IArticleService service, int id, Article article) => await service.Update(id, article))
	.WithName("UpdateArticle")
	.WithOpenApi();

app.MapDelete("/articles/{id}", async (IArticleService service, int id) => await service.Delete(id))
	.WithName("DeleteArticle")
	.WithOpenApi();

app.Run();

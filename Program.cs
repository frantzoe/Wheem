using Microsoft.AspNetCore.Identity;
using Wheem.Data;
using Wheem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger/OpenAPI -> https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<WheemContext>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddIdentityCore<IdentityUser>()
	.AddEntityFrameworkStores<WheemContext>()
	.AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGroup("/idy")
	.MapIdentityApi<IdentityUser>()
	.WithTags("Identity");

app.MapGroup("/api/products")
	.MapProductApi()
	.RequireAuthorization()
	.WithTags("Products")
	.WithOpenApi();

app.Run();

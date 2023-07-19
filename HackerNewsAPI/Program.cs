using BestStories.BusinessLogic;
using BestStories.BusinessLogic.Implementation;
using BestStories.ResourceAccess;
using BestStories.ResourceAccess.Implementation;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var url = builder.Configuration.GetSection("HackerNewsAPI").GetSection("URL");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INewsStories, NewsStories>();
builder.Services.AddHttpClient<IHackerNewsRepository, HackerNewsRepository>(option =>
{
    option.BaseAddress = new Uri(url.Value);
});
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

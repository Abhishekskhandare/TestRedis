using Microsoft.EntityFrameworkCore;
using System;
using TestRedis.DependencyInjection;
using TestRedis.EFModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();// this middleware provides the documentations with swagger but i don't see any difference with and without this.
builder.Services.AddSwaggerGen();
builder.Services.AddAutoFacProjectDependencies();
// Add Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
});
// SQL DB
builder.Services.AddDbContext<FigmentDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "My Redis Swagger";
});
#endregion

#region Redis
// Use Redis cache middleware
app.UseResponseCaching();
#endregion

app.Run();

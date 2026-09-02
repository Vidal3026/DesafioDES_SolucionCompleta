using Gestion.BL.Services;
using Gestion.Common;
using Gestion.DAL.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddControllers();
builder.Services.AddOpenApi();
//Configuración para activar Redis al levantar el servicio
//builder.Services.AddStackExchangeRedisCache(opt =>
//   opt.Configuration = builder.Configuration["AppSettings:RedisConnection"]);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddRepositoryConnector();
builder.Services.AddServiceConnector();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
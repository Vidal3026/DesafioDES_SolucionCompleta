using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Registrar Ocelot y CacheManager
builder.Services.AddOcelot()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API Gateway - Gestión Eventos");
    });
}

app.UseHttpsRedirection();

// MIDDLEWARE CLAVE: Si la petición no trae 'X-Client-Id', le asigna la IP del cliente
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.ContainsKey("X-Client-Id"))
    {
        var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "anon-client";
        context.Request.Headers["X-Client-Id"] = clientIp;
    }
    await next();
});

// Middleware de Ocelot
await app.UseOcelot();

app.Run();
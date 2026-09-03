using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar la configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2. Registrar servicios de Ocelot y CacheManager
builder.Services.AddOcelot()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// 3. Documentación OpenAPI y Scalar (debe procesarse antes del ruteo de Ocelot)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API Gateway - Gestión Eventos");
    });
}

app.UseHttpsRedirection();

// 4. Middleware personalizado: Garantizar encabezado 'X-Client-Id'
app.Use(async (context, next) =>
{
    if (!context.Request.Headers.ContainsKey("X-Client-Id"))
    {
        // Si viene detrás de un proxy/docker, intenta tomar la IP original de X-Forwarded-For
        string clientIp = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
            ?? context.Connection.RemoteIpAddress?.ToString()
            ?? "anon-client";

        context.Request.Headers["X-Client-Id"] = clientIp;
    }
    await next();
});

// 5. Middleware de Ocelot (SIEMPRE debe ir al final antes de app.Run)
await app.UseOcelot();

app.Run();
using DinamicaComercial.Server;
using DinamicaComercial.Server.Application.Interfaces;
using DinamicaComercial.Server.Endpoints;
using DinamicaComercial.Server.Features.Catalogs;
using DinamicaComercial.Server.Features.Dash;
using DinamicaComercial.Server.Features.Dash.Get;
using DinamicaComercial.Server.Features.DecrementoVentas;
using DinamicaComercial.Server.Middlewares;
using DinamicaComercial.Server.Persistence;
using DinamicaComercial.Server.Persistence.Repositories;
using DinamicaComercial.Server.Persistence.Services;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();

        builder.Services.AddScoped<IDashSender, GetDashHandler>();
        builder.Services.AddScoped<IDashRepository, DashRepository>();
        builder.Services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        builder.Services.AddScoped<IDbConnectionProvider, DbConnectionProvider>();
        builder.Services.AddScoped<ICatalogsRepository, CatalogsRepository>();
        builder.Services.AddScoped<IDatabaseResolverService, DatabaseResolverService>();
        builder.Services.AddScoped<IDecrementoVentasRepository, DecrementoVentasRepository>();
        builder.Services.AddScoped<ConfigState>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseDefaultFiles();
        app.UseStaticFiles();

        DashEndpoint.Map(app);
        CatalogsEndpoint.Map(app);
        DecrementoVentasEndpoint.Map(app);

        app.UseMiddleware<ConfigMiddleware>();

        app.MapFallbackToFile("/index.html");
       
        app.Run();
    }
}
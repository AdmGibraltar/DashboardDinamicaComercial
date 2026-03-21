using DinamicaComercial.Server;
using DinamicaComercial.Server.Dash;
using DinamicaComercial.Server.Dash.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IDataService, DataService>();
var isCentralizedMode = builder.Configuration.GetValue<bool>("IsCentralizedMode");
if (isCentralizedMode)
    builder.Services.AddScoped<IDashRepository, SianwebCentralRepository>();
else builder.Services.AddScoped<IDashRepository, SianwebRepository>();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

Endpoints.MapEndpoints(app);

app.MapFallbackToFile("/index.html");

app.Run();
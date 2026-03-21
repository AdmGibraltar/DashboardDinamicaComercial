using DinamicaComercial.Server.Dash;

namespace DinamicaComercial.Server
{
    public static class Endpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/metrics", async (
                [AsParameters] DashQuery query,
                IDataService dataService) =>
            {
                var metrics = await dataService.GetDashMetricsAsync(query);
                return Results.Ok(metrics);
            })
            .WithName("GetMetrics");

            app.MapGet("/riks", async () =>
            {
                
            });
        }
    }
}
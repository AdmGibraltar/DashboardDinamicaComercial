using DinamicaComercial.Server.Features.Dash;
using DinamicaComercial.Server.Features.Dash.Get;

namespace DinamicaComercial.Server.Endpoints
{
    public static class DashEndpoint
    {
        public static void Map(this IEndpointRouteBuilder app)
        {
            var apiGroup = app.MapGroup("/api");

            apiGroup.MapGet("/metrics", async (
                [AsParameters] GetDashQuery query,
                IDashSender mediator) =>
            {
                var metrics = await mediator.Send(query);
                return Results.Ok(metrics);
            })
            .WithName("GetMetrics");

            apiGroup.MapGet("/metrics-kpi3", async (
                [AsParameters] GetDashQuery query,
                IDashRepository repo) =>
            {
                var kpi3 = await repo.GetKpi3Async(query);
                return Results.Ok(kpi3);
            })
            .WithName("GetKpi3");
        }
    }
}

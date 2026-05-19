using DinamicaComercial.Server.Features.DecrementoVentas;
using DinamicaComercial.Server.Features.DecrementoVentas.AddMotivo;
using DinamicaComercial.Server.Features.DecrementoVentas.Get;

namespace DinamicaComercial.Server.Endpoints
{
    public static class DecrementoVentasEndpoint
    {
        public static void Map(this IEndpointRouteBuilder app)
        {
            var apiGroup = app.MapGroup("/api");

            apiGroup.MapGet("/decremento-ventas", async (
                [AsParameters] DecrementoVentasQuery query,
                IDecrementoVentasRepository repo) =>
            {
                var result = await repo.Get(query);
                return Results.Ok(result);
            }).WithName("GetDecrementoVentas");

            apiGroup.MapPost("/decremento-ventas", async (
                AddMotivoQuery query,
                IDecrementoVentasRepository repo) =>
            {
                if (query.Motivos == null || query.Motivos.Count == 0)
                    return Results.BadRequest("La lista no puede estar vacía");

                var result = await repo.AddMotivos(query.Motivos);
                return Results.Ok(result);
            }).WithName("AddDecrementoVentas");
        }
    }
}

using DinamicaComercial.Server.Features.Catalogs;

namespace DinamicaComercial.Server.Endpoints
{
    public static class CatalogsEndpoint
    {
        public static void Map(this IEndpointRouteBuilder app)
        {
            var apiGroup = app.MapGroup("/api");
            
            ///SUCURSALES-GRUPO
            apiGroup.MapGet("/sucursales-group", async () =>
            {
                List<CatalogsResponse> g = [
                    new(1, "CDI Propios"),
                    new(2, "CDC Propios"),
                    new(3, "CDI Franquicias"),
                    new(4, "CDC Franquicias")
                ];
                return Results.Ok(g);
            }).WithName("GetSucursalesGroup");

            ///SUCURSALES
            apiGroup.MapGet("/sucursales", async (
                int groupId,
                ICatalogsRepository repo) =>
            {
                var sucursales = await repo.GetSucursalesAsync(groupId);
                var sucursalesF = sucursales
                    .Select(s => 
                        new CatalogsResponse(s.Id, s.Name.Replace("-", " - "))
                     )
                    .ToList();
                return Results.Ok(sucursalesF);
            }).WithName("GetSucursales");

            ///RIKS
            apiGroup.MapGet("/riks", async (
                [AsParameters] CatalogsQuery query,
                ICatalogsRepository repo) =>
            {
                var riks = await repo.GetRiksAsync(query);
                return Results.Ok(riks);
            }).WithName("GetRiks");

            ///UENS
            apiGroup.MapGet("/uens", async (
                [AsParameters] CatalogsQuery query,
                ICatalogsRepository repo) =>
            {
                var uens = await repo.GetUensAsync(query);
                return Results.Ok(uens);
            }).WithName("GetUens");

            ///SEGMENTOS
            apiGroup.MapGet("/segmentos", async (
                [AsParameters] CatalogsQuery query,
                ICatalogsRepository repo) =>
            {
                var segmentos = await repo.GetSegmentosAsync(query);
                return Results.Ok(segmentos);
            }).WithName("GetSegmentos");

            ///MOTIVOS DECREMENTO
            apiGroup.MapGet("/motivos-decremento", async (ICatalogsRepository repo) =>
            {
                var motivos = await repo.GetMotivosDecrementoAsync();
                return Results.Ok(motivos);
            }).WithName("GetMotivosDecremento");

            ///CAUSAS DECREMENTO
            apiGroup.MapGet("/causas-decremento", async (ICatalogsRepository repo) =>
            {
                var causas = await repo.GetCausasDecrementoAsync();
                return Results.Ok(causas);
            }).WithName("GetCausasDecremento");
        }
    }
}

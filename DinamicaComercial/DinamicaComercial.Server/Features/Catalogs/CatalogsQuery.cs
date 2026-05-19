namespace DinamicaComercial.Server.Features.Catalogs
{
    public record CatalogsQuery(
        int SucursalId,
        bool? IsGerente,
        int? IdUser,
        int? UenId);
}

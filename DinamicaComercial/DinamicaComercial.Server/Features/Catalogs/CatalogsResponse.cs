namespace DinamicaComercial.Server.Features.Catalogs
{
    public record CatalogsResponse(int Id, string Name);
    public record CatalogsResponseCausasDecremento(
        int Id,
        string Name,
        int IdMotivo);
}

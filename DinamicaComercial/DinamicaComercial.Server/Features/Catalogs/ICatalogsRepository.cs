namespace DinamicaComercial.Server.Features.Catalogs
{
    public interface ICatalogsRepository
    {
        Task<IReadOnlyList<CatalogsResponse>> GetSucursalesAsync(int idSucursalGroup);
        Task<IReadOnlyList<CatalogsResponse>> GetRiksAsync(CatalogsQuery query);
        Task<IReadOnlyList<CatalogsResponse>> GetUensAsync(CatalogsQuery query);
        Task<IReadOnlyList<CatalogsResponse>> GetSegmentosAsync(CatalogsQuery query);
        Task<IReadOnlyList<CatalogsResponse>> GetMotivosDecrementoAsync();
        Task<IReadOnlyList<CatalogsResponseCausasDecremento>> GetCausasDecrementoAsync();
    }
}

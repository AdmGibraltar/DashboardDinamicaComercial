namespace DinamicaComercial.Server.Application.Interfaces
{
    public interface IDatabaseResolverService
    {
        Task<string?> GetDbNameAsync(int sucursalId);
    }
}

using System.Data;

namespace DinamicaComercial.Server.Application.Interfaces
{
    public interface IDbConnectionProvider
    {
        Task<IDbConnection> GetConnection(string? sucursalId);
    }
}

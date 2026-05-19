using Dapper;
using DinamicaComercial.Server.Application.Interfaces;

namespace DinamicaComercial.Server.Persistence.Services
{
    public class DatabaseResolverService(ISqlConnectionFactory factory) : IDatabaseResolverService
    {
        public async Task<string?> GetDbNameAsync(int sucursalId)
        {
            using var db = factory.CreateSIANCENTRALConnection();
            try
            {
                return await db.QueryFirstOrDefaultAsync<string>(
                    "SELECT Db_Nombre FROM CatSucursal WHERE Id_Cd = @sucursalId",
                    new { sucursalId }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching riks data: {ex.Message}");
                return null;
            }
        }
    }
}

using DinamicaComercial.Server.Application.Interfaces;
using System.Data;

namespace DinamicaComercial.Server.Persistence
{
    public class DbConnectionProvider(
        ConfigState configState,
        IDatabaseResolverService databaseResolverService,
        ISqlConnectionFactory factory
        ) : IDbConnectionProvider
    {
        public async Task<IDbConnection> GetConnection(string? sucursalId)
        {
            return configState.Mode switch
            {
                ConfigState.ApplicationMode.siancentral => factory.CreateSIANWEBCENTRALConnection(),
                ConfigState.ApplicationMode.sianweb => await PrepareSianwebConnection(sucursalId),
                _ => throw new InvalidOperationException(
                    "No se pudo establecer una conexión porque el modo de aplicacion (header 'mode') no es válido o no fue especificado.")
            };
        }

        private async Task<IDbConnection> PrepareSianwebConnection(string? sucursalId)
        {
            if (string.IsNullOrEmpty(sucursalId))
                throw new InvalidOperationException("no se pudo establecer una conexion a sianweb, porque no se ha especificado la sucursal Id");

            string? dbName = await databaseResolverService.GetDbNameAsync(int.Parse(sucursalId));
            return dbName is null
                ? throw new InvalidOperationException("no se encontro el nombre de la bd")
                : factory.CreateSIANWEBConnection(dbName);
        }
    }
}

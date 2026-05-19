using DinamicaComercial.Server.Application.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DinamicaComercial.Server.Persistence
{
    public class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
    {
        public IDbConnection CreateSIANCENTRALConnection() =>
            new SqlConnection(configuration.GetConnectionString("SIANCENTRAL"));

        public IDbConnection CreateSIANWEBCENTRALConnection() =>
            new SqlConnection(configuration.GetConnectionString("SIANWEBCENTRAL"));

        public IDbConnection CreateSIANWEBConnection(string dbName)
        {
            string baseConnectionString = configuration.GetConnectionString("SIANWEB")
                ?? throw new InvalidOperationException("No se ha especificado la conexion de SIANWEB");

            bool useDefault = bool.Parse(configuration.GetSection("DbSettings:UseDefaultDatabase").Value ?? "false");
            string defaultDb = configuration.GetSection("DbSettings:DefaultDatabase").Value ?? "";

            string targetDatabase;
            if (useDefault && !string.IsNullOrEmpty(defaultDb))
                targetDatabase = defaultDb;
            else
            {
                targetDatabase = dbName;
            }
            var builder = new SqlConnectionStringBuilder(baseConnectionString) { InitialCatalog = targetDatabase };
            return new SqlConnection(builder.ConnectionString);
        }
    }
}

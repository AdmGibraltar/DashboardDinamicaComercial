using System.Data;

namespace DinamicaComercial.Server.Application.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateSIANWEBConnection(string dbName);
        IDbConnection CreateSIANWEBCENTRALConnection();
        IDbConnection CreateSIANCENTRALConnection();
    }
}

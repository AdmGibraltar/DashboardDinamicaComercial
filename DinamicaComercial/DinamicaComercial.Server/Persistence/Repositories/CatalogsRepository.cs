using Dapper;
using DinamicaComercial.Server.Application.Interfaces;
using DinamicaComercial.Server.Features.Catalogs;

namespace DinamicaComercial.Server.Persistence.Repositories
{
    public class CatalogsRepository(
        ISqlConnectionFactory factory,
        IDatabaseResolverService databaseResolverService) : ICatalogsRepository
    {
        public async Task<IReadOnlyList<CatalogsResponseCausasDecremento>> GetCausasDecrementoAsync()
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                string sql = @"SELECT Id, Causa AS Name, IdMotivo
                            FROM CausaDecrementosVenta";
                var dbos = await db.QueryAsync<CatalogsResponseCausasDecremento>(sql);

                return dbos?.ToList().AsReadOnly() ?? new List<CatalogsResponseCausasDecremento>().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching causas data: {ex.Message}");
                return [];
            }
        }

        public async Task<IReadOnlyList<CatalogsResponse>> GetMotivosDecrementoAsync()
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                string sql = @$"SELECT Id AS Id, Motivo AS Name FROM MotivoDecrementosVenta";
                var dbos = await db.QueryAsync<CatalogsResponse>(sql);

                return dbos?.ToList().AsReadOnly() ?? new List<CatalogsResponse>().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching motivos decremento data: {ex.Message}");
                return [];
            }
        }

        public async Task<IReadOnlyList<CatalogsResponse>> GetRiksAsync(CatalogsQuery query)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                string? dbName = await databaseResolverService.GetDbNameAsync(query.SucursalId);
                if (dbName is null)
                {
                    return [];
                }
                string sql = @$"SELECT r.Id_Rik  AS Id
                            , r.Rik_Nombre      AS Name
                            FROM {dbName}..CatRik r
                            JOIN {dbName}..CatUsuario u ON
                                r.Id_Rik = u.Id_Rik
                            WHERE r.Id_Cd = @sucursalId
                                AND r.Rik_Activo = 1
                                AND u.U_Activo = 1";
                if (!(query.IsGerente ?? false))
                {
                    sql += $" AND u.Id_U = @idUser";
                }

                var dbos = await db.QueryAsync<CatalogsResponse>(sql, new
                {
                    sucursalId = query.SucursalId,
                    idUser = query.IdUser
                });

                return dbos?.ToList().AsReadOnly() ?? new List<CatalogsResponse>().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching riks data: {ex.Message}");
                return [];
            }
        }

        public async Task<IReadOnlyList<CatalogsResponse>> GetSegmentosAsync(CatalogsQuery query)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                string? dbName = await databaseResolverService.GetDbNameAsync(query.SucursalId);
                if (dbName is null)
                {
                    return [];
                }
                string sql = @$"SELECT Id_Seg    AS Id
                            , Seg_Descripcion   AS Name
                            FROM {dbName}..CatSegmento
                            WHERE Id_Uen = @uenId";

                var dbos = await db.QueryAsync<CatalogsResponse>(sql, new
                {
                    uenId = query.UenId
                });

                return dbos?.ToList().AsReadOnly() ?? new List<CatalogsResponse>().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching uens data: {ex.Message}");
                return [];
            }
        }

        public async Task<IReadOnlyList<CatalogsResponse>> GetSucursalesAsync(int idSucursalGroup)
        {
            using var db = factory.CreateSIANCENTRALConnection();
            try
            {
                string sql = @"SELECT DISTINCT Id_Cd AS Id
                                    , Sucursal AS Name
                                FROM CatSucursal
                                WHERE Id_Grupo = @g";
                if (idSucursalGroup == 1)
                {
                    sql += " AND Id_Cd NOT IN (108, 130, 320, 380, 710)";
                }
                if (idSucursalGroup == 3)
                {
                    sql += " AND Id_Cd NOT IN (830, 31164, 32110)";
                }
                if (idSucursalGroup == 4)
                {
                    sql += " AND Id_Cd NOT IN (830, 34101)";
                }

                var dbos = await db.QueryAsync<CatalogsResponse>(sql, new
                {
                    g = idSucursalGroup
                });
                return dbos?.ToList().AsReadOnly() ?? new List<CatalogsResponse>().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sucursales data: {ex.Message}");
                return [];
            }
        }

        public async Task<IReadOnlyList<CatalogsResponse>> GetUensAsync(CatalogsQuery query)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                string? dbName = await databaseResolverService.GetDbNameAsync(query.SucursalId);
                if (dbName is null)
                {
                    return [];
                }
                string sql = @$"SELECT Id_Uen    AS Id
                            , Uen_Descripcion   AS Name
                            FROM {dbName}..CatUen
                            WHERE Id_Uen NOT IN (5, 6)";

                Console.WriteLine(sql);

                var dbos = await db.QueryAsync<CatalogsResponse>(sql);

                return dbos?.ToList().AsReadOnly() ?? new List<CatalogsResponse>().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching uens data: {ex.Message}");
                return [];
            }
        }
    }
}

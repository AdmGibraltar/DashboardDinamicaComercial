using Dapper;
using DinamicaComercial.Server.Application.Interfaces;
using DinamicaComercial.Server.Features.Dash;
using DinamicaComercial.Server.Features.Dash.Get;
using System.Data;

namespace DinamicaComercial.Server.Persistence.Repositories
{
    public class DashRepository(
        IConfiguration configuration,
        ISqlConnectionFactory factory
    ) : IDashRepository
    {
        public async Task<IReadOnlyList<Kpi1>> GetKpi1Async(GetDashQuery query)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                var parameters = new
                {
                    year = query.Year,
                    month = query.Month,
                    uenId = query.IdUen,
                    segmentoId = query.IdSeg,
                    rikId = query.RikId,
                    sucursalesId = query.SucursalId
                };

                var dbos = await db.QueryAsync<Kpi1>(
                "sp_dinamicaComercialKpi1",
                parameters,
                commandType: CommandType.StoredProcedure);

                if (dbos is null) return [];

                return [.. dbos];

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching KPI1 data: {ex.Message}");
                return [];
            }
        }

        public async Task<IReadOnlyList<Kpi2>> GetKpi2Async(GetDashQuery query)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                var parameters = new
                {
                    year = query.Year,
                    month = query.Month,
                    uenId = query.IdUen,
                    segmentoId = query.IdSeg,
                    rikId = query.RikId,
                    sucursalesId = query.SucursalId
                };

                var dbos = await db.QueryAsync<Kpi2>(
                "sp_dinamicaComercialKpi2",
                parameters,
                commandType: CommandType.StoredProcedure);

                if (dbos is null) return [];

                return [.. dbos];

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching KPI1 data: {ex.Message}");
                return [];
            }
        }

        public async Task<IReadOnlyList<Kpi3>> GetKpi3Async(GetDashQuery query)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                var parameters = new
                {
                    sucursalesId = query.SucursalId,
                    month = query.Month,
                    year = query.Year,
                    rikId = query.RikId
                };

                var dbos = await db.QueryAsync<Kpi3>(
                "sp_dinamicaComercialKpi3",
                parameters,
                commandType: CommandType.StoredProcedure);

                if (dbos is null) return [];

                return [.. dbos];

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching KPI3 data: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// tanto para sianweb y siancentral dejamos fijo la conexion
        /// a mty
        /// </summary>
        public async Task<bool> IsClosingMonthAsync(int monthFilter, int yearFilter)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                string defaultDbToReadCalendarKey = configuration.GetSection("DefaultDbToReadCalendarKey").Value ?? "sianwebmty";
                var currentMonthRef = await db.QueryFirstOrDefaultAsync<(int Year, int Month)>(
                    $"SELECT Cal_Año, Cal_Mes FROM {defaultDbToReadCalendarKey}..CatCalendario WHERE Cal_Actual = 1"
                );

                if (currentMonthRef == default)
                {
                    Console.WriteLine("No current month found in CatCalendario");
                    return false;
                }

                DateTime date = new(currentMonthRef.Year, currentMonthRef.Month, 1);
                DateTime closeDate = date.AddMonths(-1);

                return monthFilter == closeDate.Month && yearFilter == closeDate.Year;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking current month: {ex.Message}");
                return false;
            }
        }
    }
}

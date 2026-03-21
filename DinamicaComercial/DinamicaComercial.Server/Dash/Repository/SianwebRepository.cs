using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DinamicaComercial.Server.Dash.Repository
{
    public class SianwebRepository(IConfiguration configuration) : IDashRepository
    {
        private IDbConnection CreateConnection => new SqlConnection(configuration.GetConnectionString("SIANWEB"));
        public async Task<IReadOnlyList<Kpi1>> GetKpi1Async(DashQuery query)
        {
            using var db = CreateConnection;
            try
            {
                var parameters = new
                {
                    year = query.Year,
                    month = query.Month,
                    idUen = query.IdUen,
                    idSeg = query.IdSeg,
                    rikId = query.RikId
                };

                var dbos = await db.QueryAsync<Kpi1>(
                "sp_dinamicaComercialKpi1",
                parameters,
                commandType: CommandType.StoredProcedure);

                if (dbos is null) return [];

                return [.. dbos];
    
            } catch (Exception ex)
            {
                Console.WriteLine($"Error fetching KPI1 data: {ex.Message}");
                return [];
            }
        }
    }
}
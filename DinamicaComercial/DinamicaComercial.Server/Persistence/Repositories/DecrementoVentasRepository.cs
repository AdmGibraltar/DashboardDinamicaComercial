using Dapper;
using DinamicaComercial.Server.Application.Interfaces;
using DinamicaComercial.Server.Features.DecrementoVentas;
using DinamicaComercial.Server.Features.DecrementoVentas.AddMotivo;
using DinamicaComercial.Server.Features.DecrementoVentas.Get;

namespace DinamicaComercial.Server.Persistence.Repositories
{
    public class DecrementoVentasRepository(
        IConfiguration configuration,
        ISqlConnectionFactory factory) : IDecrementoVentasRepository
    {
        public async Task<bool> AddMotivos(List<AddMotivoItems> motivos)
        {
            using var db = factory.CreateSIANWEBCENTRALConnection();
            try
            {
                string sql = @$"UPDATE ClientesDecrementosVenta
                                SET IdMotivo = @idMotivo
                                , IdCausa = @idCausa
                                , FechaActualizacion = GETDATE()
                                WHERE Id = @idDecrementoVenta";
                int rows = await db.ExecuteAsync(sql, motivos);
                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando motivos de decremento de ventas: {ex.Message}");
                return false;
            }
        }

        public async Task<IReadOnlyList<DecrementoVentasResponse>> Get(DecrementoVentasQuery query)
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
                    return [];
                }

                DateTime date = new(currentMonthRef.Year, currentMonthRef.Month, 1);
                DateTime closeDate = date.AddMonths(-1);

                string sql = @$"SELECT Id
                                , Id_Rik AS IdRik
                                , Rik, Id_Cte AS IdCliente
                                , Cliente
                                , ROUND(VentaMes, 0) AS VentaMes
                                , ROUND(PromedioVenta6M, 0) AS PromedioVenta6M
                                , ROUND(PorcentajeCaida, 0) AS PorcentajeDecremento
                                , IdMotivo
                                , IdCausa
                                , CAST(CASE
                                    WHEN FechaActualizacion IS NOT NULL THEN 1
                                    ELSE 0
                                END AS BIT) AS Completado
                                FROM ClientesDecrementosVenta
                                WHERE Id_Cd = @sucursalId
                                    AND CalendarioKeyMes = @month 
                                    AND CalendarioKeyAnio = @year";
                if (query.RikId.HasValue)
                {
                    sql += $" AND Id_Rik = @rikId";
                }

                Console.WriteLine(sql);
                int monthTemp = 2;
                var dbos = await db.QueryAsync<DecrementoVentasResponse>(sql, new
                {
                    month = monthTemp,//closeDate.Month,
                    year = closeDate.Year,
                    sucursalId = query.SucursalId,
                    rikId = query.RikId
                });

                return dbos?.ToList().AsReadOnly() ?? new List<DecrementoVentasResponse>().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching decrementos venta data: {ex.Message}");
                return [];
            }
        }
    }
}

using DinamicaComercial.Server.Extensions;

namespace DinamicaComercial.Server.Dash
{
    public class DataService(IDashRepository dashRepository) : IDataService
    {
        public async Task<DashResponse> GetDashMetricsAsync(DashQuery query)
        {
            try
            {
                var kpi1 = dashRepository.GetKpi1Async(query).SafeExecute([]);

                await Task.WhenAll(
                    kpi1
                );

                var kpi1Result = await kpi1 ?? [];
                
                return new DashResponse(
                    [.. kpi1Result]
                );
            }
            catch
            {
                return new DashResponse([]);
            }
        }
    }
}
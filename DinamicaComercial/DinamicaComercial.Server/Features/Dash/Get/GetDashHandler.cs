using DinamicaComercial.Server.Extensions;

namespace DinamicaComercial.Server.Features.Dash.Get
{
    public class GetDashHandler(IDashRepository dashRepository) : IDashSender
    {
        public async Task<DashResponse> Send(GetDashQuery query)
        {
            try
            {
                var isClosingMonth = dashRepository
                    .IsClosingMonthAsync(query.Month, query.Year)
                    .SafeExecute(false);

                var kpi1 = dashRepository.GetKpi1Async(query).SafeExecute([]);
                var kpi2 = dashRepository.GetKpi2Async(query).SafeExecute([]);
                var kpi3 = dashRepository.GetKpi3Async(query).SafeExecute([]);

                await Task.WhenAll(
                    isClosingMonth,
                    kpi1,
                    kpi2,
                    kpi3);

                bool isClosingMonthResult = await isClosingMonth;
                var kpi1Result = await kpi1 ?? [];
                var kpi2Result = await kpi2 ?? [];
                var kpi3Result = await kpi3 ?? [];

                return new DashResponse(
                    isClosingMonthResult,
                    [..kpi1Result],
                    [..kpi2Result],
                    [..kpi3Result]);
            }
            catch { return new DashResponse(false, [], [], []); }
        }
    }
}

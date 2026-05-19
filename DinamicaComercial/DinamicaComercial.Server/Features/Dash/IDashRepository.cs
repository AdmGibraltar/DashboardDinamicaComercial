using DinamicaComercial.Server.Features.Dash.Get;

namespace DinamicaComercial.Server.Features.Dash
{
    public interface IDashRepository
    {
        Task<bool> IsClosingMonthAsync(int monthFilter, int yearFilter);
        Task<IReadOnlyList<Kpi1>> GetKpi1Async(GetDashQuery query);
        Task<IReadOnlyList<Kpi2>> GetKpi2Async(GetDashQuery query);
        Task<IReadOnlyList<Kpi3>> GetKpi3Async(GetDashQuery query);
    }
}

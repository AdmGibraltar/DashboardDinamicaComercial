namespace DinamicaComercial.Server.Dash
{
    public interface IDashRepository
    {
        Task<IReadOnlyList<Kpi1>> GetKpi1Async(DashQuery query);
    }
}
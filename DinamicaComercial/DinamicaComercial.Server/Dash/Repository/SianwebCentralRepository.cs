namespace DinamicaComercial.Server.Dash.Repository
{
    public class SianwebCentralRepository : IDashRepository
    {
        Task<IReadOnlyList<Kpi1>> IDashRepository.GetKpi1Async(DashQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
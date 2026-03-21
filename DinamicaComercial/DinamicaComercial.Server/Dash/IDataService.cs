namespace DinamicaComercial.Server.Dash
{
    public interface IDataService
    {
        Task<DashResponse> GetDashMetricsAsync(DashQuery query);
    }
}
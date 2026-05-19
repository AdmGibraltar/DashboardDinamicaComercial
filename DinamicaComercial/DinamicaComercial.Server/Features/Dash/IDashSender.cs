using DinamicaComercial.Server.Features.Dash.Get;

namespace DinamicaComercial.Server.Features.Dash
{
    public interface IDashSender
    {
        Task<DashResponse> Send(GetDashQuery query);
    }
}

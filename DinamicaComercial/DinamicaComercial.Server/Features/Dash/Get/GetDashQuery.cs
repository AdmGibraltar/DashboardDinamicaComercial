namespace DinamicaComercial.Server.Features.Dash.Get
{
    public record GetDashQuery(
        int Year,
        int Month,
        int? IdUen,
        int? IdSeg,
        int? RikId,
        string? SucursalId);
}

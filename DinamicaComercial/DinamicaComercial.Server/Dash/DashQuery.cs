namespace DinamicaComercial.Server.Dash
{
    public record DashQuery(
        int Year,
        int Month,
        int? IdUen,
        int? IdSeg,
        int? RikId,
        int? SucursalId
    );
}
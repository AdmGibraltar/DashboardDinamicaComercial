namespace DinamicaComercial.Server.Dash
{
    public record DashResponse(
        List<Kpi1> Kpi1
    );

    public record Kpi1(
        string Segmento,
        int Clientes,
        int Clientes6M,
        decimal TotalVentas6M,
        decimal TotalVentas,
        decimal CrecimientoAbsoluto,
        decimal CrecimientoPorcentual
    );
}
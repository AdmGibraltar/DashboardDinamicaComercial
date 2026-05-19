namespace DinamicaComercial.Server.Features.Dash.Get
{
    public record DashResponse(
        bool IsClosingMonth,
        List<Kpi1> Kpi1,
        List<Kpi2> Kpi2,
        List<Kpi3> Kpi3);

    public record Kpi1(
        string Segmento,
        int Clientes,
        int Clientes6M,
        decimal TotalVentas6M,
        decimal TotalVentas,
        decimal CrecimientoAbsoluto,
        decimal CrecimientoPorcentual
    );

    public record Kpi2(
        string Segmento,
        int Clientes,
        int Clientes6M,
        decimal TotalVentas6M,
        decimal TotalVentas,
        decimal CrecimientoAbsoluto,
        decimal CrecimientoPorcentual,
        decimal PorcentajeVentas
    );

    public record Kpi3(
        string Motivo,
        string Causa,
        int TotalClientes,
        decimal TotalVentaMes,
        decimal TotalPromedio6M,
        decimal TotalDiferencia,
        decimal PorcentajeDecremento);
}

namespace DinamicaComercial.Server.Features.DecrementoVentas.Get
{
    public record DecrementoVentasResponse(
        int Id,
        int IdRik,
        string Rik,
        int IdCliente,
        string Cliente,
        decimal VentaMes,
        decimal PromedioVenta6M,
        decimal PorcentajeDecremento,
        int? IdMotivo,
        int? IdCausa,
        bool Completado);
}

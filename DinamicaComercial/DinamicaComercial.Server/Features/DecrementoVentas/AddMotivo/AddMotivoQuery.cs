namespace DinamicaComercial.Server.Features.DecrementoVentas.AddMotivo
{
    public record AddMotivoQuery(List<AddMotivoItems> Motivos);

    public record AddMotivoItems(
        int IdDecrementoVenta,
        int IdMotivo,
        int IdCausa);
}

using DinamicaComercial.Server.Features.DecrementoVentas.AddMotivo;
using DinamicaComercial.Server.Features.DecrementoVentas.Get;

namespace DinamicaComercial.Server.Features.DecrementoVentas
{
    public interface IDecrementoVentasRepository
    {
        Task<IReadOnlyList<DecrementoVentasResponse>> Get(DecrementoVentasQuery query);
        Task<bool> AddMotivos(List<AddMotivoItems> motivos);
    }
}

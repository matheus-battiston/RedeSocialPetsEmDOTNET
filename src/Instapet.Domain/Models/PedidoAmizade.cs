namespace Instapet.Domain.Models;

public class PedidoAmizade : Base
{
    public PedidoAmizade(Guid requerenteId, Guid requisitadoId)
    {
        RequerenteId = requerenteId;
        RequisitadoId = requisitadoId;
    }

    public Guid RequerenteId { get; private set; }
    public Usuario Requerente { get; set; }

    public Guid RequisitadoId { get; private set; }
    public Usuario Requisitado { get; set; }
}
using Instapet.Domain.Validations;

namespace Instapet.Domain.Contracts.Responses;

public class PedidoAmizadeResponse : Notifiable
{
    public DetalhesUsuarioResponse Requerente { get; set; }
    public Guid IdPedido { get; set; }
}
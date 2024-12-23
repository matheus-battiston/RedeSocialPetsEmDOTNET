using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Models;

namespace Instapet.Application.Implementations.Mappers;

public class PedidoAmizadeMapper
{
    public static PedidoAmizadeResponse ToResponse(PedidoAmizade entity)
    {
        var usuario = UsuarioMapper.ToResponse(entity.Requerente);
        return new PedidoAmizadeResponse { IdPedido = entity.Id, Requerente = usuario };
    }

    public static PedidoAmizade ToEntity(Guid idAmigo, Guid idAutenticado)
    {
        return new PedidoAmizade(idAutenticado, idAmigo);
    }
}
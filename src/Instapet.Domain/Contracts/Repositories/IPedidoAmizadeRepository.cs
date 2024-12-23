using Instapet.Domain.Models;

namespace Instapet.Domain.Contracts.Repositories;

public interface IPedidoAmizadeRepository
{
    void Adicionar(PedidoAmizade pedidoAmizade);
    void RemoverPedido(PedidoAmizade pedido);

    PedidoAmizade? Obter(Guid pedidoAmizade);
}
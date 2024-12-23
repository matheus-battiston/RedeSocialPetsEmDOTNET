using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Models;
using Instapet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Instapet.Infrastructure.Repositories;

public class PedidoAmizadeRepository : IPedidoAmizadeRepository
{
    private readonly DataContext _context;

    public PedidoAmizadeRepository(DataContext context)
    {
        _context = context;
    }

    public void Adicionar(PedidoAmizade pedidoAmizade)
    {
        _context.PedidoAmizades.Add(pedidoAmizade);
        _context.SaveChangesAsync();
    }

    public void RemoverPedido(PedidoAmizade pedido)
    {
        _context.PedidoAmizades.Remove(pedido);
        _context.SaveChangesAsync();
    }

    public PedidoAmizade? Obter(Guid id)
        => _context.PedidoAmizades
            .Include(p => p.Requerente)
            .Include(p => p.Requisitado)
            .FirstOrDefault(p => p.Id == id);
}
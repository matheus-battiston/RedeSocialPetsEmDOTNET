using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Models;
using Instapet.Infrastructure.Data;

namespace Instapet.Infrastructure.Repositories;

public class UsuarioAmigoRepository : IUsuarioAmigoRepository
{
    private readonly DataContext _context;

    public UsuarioAmigoRepository(DataContext context)
    {
        _context = context;
    }

    public void AdicionarAmigo(UsuarioAmigo amizadeUm, UsuarioAmigo amizadeDois)
    {
        _context.UsuarioAmigos.Add(amizadeUm);
        _context.UsuarioAmigos.Add(amizadeDois);
        _context.SaveChangesAsync();
    }

    public void RemoverAmizade(Guid idAutenticado, Guid idAmigo)
    {
        var amizadeUm = _context.UsuarioAmigos
            .FirstOrDefault(u => u.UsuarioId == idAutenticado && u.AmigoId == idAmigo);
        
        var amizadeDois = _context.UsuarioAmigos
            .FirstOrDefault(u => u.UsuarioId == idAmigo && u.AmigoId == idAutenticado);

        if (amizadeUm != null) _context.UsuarioAmigos.Remove(amizadeUm);
        if (amizadeDois != null) _context.UsuarioAmigos.Remove(amizadeDois);
        _context.SaveChangesAsync();
    }
}
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Models;
using Instapet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Instapet.Infrastructure.Repositories;

public class ComentarioRepository : IComentarioRepository
{
    private readonly DataContext _context;

    public ComentarioRepository(DataContext context)
    {
        _context = context;
    }

    public void AdicionarComentario(Comentario comentario)
    {
        _context.Comentarios.Add(comentario);
        _context.SaveChangesAsync();
    }
    
    public List<Comentario> ListarComentarios(Guid idPost)
    {
        return _context.Comentarios
            .Where(u => u.IdPost == idPost)
            .Include(c => c.Usuario)
            .ToList();    }
}
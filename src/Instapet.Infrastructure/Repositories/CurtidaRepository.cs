using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Models;
using Instapet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Instapet.Infrastructure.Repositories;

public class CurtidaRepository : ICurtidaRepository
{
    private readonly DataContext _context;

    public CurtidaRepository(DataContext context)
    {
        _context = context;
    }

    public void Adicionar(Curtida curtida)
    {
        _context.Curtidas.Add(curtida);
        _context.SaveChangesAsync();
    }

    public bool JaCurtiu(Guid idUsuario, Guid idPost)
    {
        return _context.Curtidas
            .Include(curtida => curtida.Usuario)
            .Include(curtida => curtida.Post)
            .Any(curtida => curtida.Usuario.Id == idUsuario && curtida.Post.Id == idPost);
    }

    public void RemoverCurtida(Guid idUsuario, Guid idPost)
    {
        var curtida =
            _context.Curtidas.FirstOrDefault(curtida => curtida.IdPost == idPost && curtida.IdUsuario == idUsuario);
        if (curtida != null) _context.Curtidas.Remove(curtida);

        _context.SaveChangesAsync();
    }
}
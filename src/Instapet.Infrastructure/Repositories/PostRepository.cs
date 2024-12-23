using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Models;
using Instapet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Instapet.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DataContext _context;

    public PostRepository(DataContext context)
    {
        _context = context;
    }

    public void Adicionar(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChangesAsync();
    }

    public bool ExistePost(Guid idPost)
    {
        return _context.Posts.Any(p => p.Id == idPost);
    }

    public bool TemAcessoPost(Guid idUsuario, Guid idPost)
    {
        return _context.Posts
            .Where(post => post.Id == idPost)
            .Include(post => post.Usuario)
            .ThenInclude(donoPost => donoPost.Amigos)
            .Any(post => post.Usuario.Id == idUsuario || !post.Privado || post.Usuario.Amigos.Any(amigo => amigo.Id == idUsuario) );
    }

    public List<Post> ListarPostsHome(Guid idUsuario, int size, int page)
    {
        return _context.Posts
            .Where(post => post.Usuario.Id == idUsuario || post.Usuario.Amigos.Any(amigo => amigo.Id == idUsuario))
            .Include(post => post.Curtidas)
            .ThenInclude(curtida => curtida.Usuario )
            .Include(post => post.Usuario)
            .ThenInclude(usuario => usuario.Amigos)
            .OrderByDescending(post => post.Horario)
            // .Skip(size*page)
            .Take(size * (page + 1))
            .ToList();
    }
}
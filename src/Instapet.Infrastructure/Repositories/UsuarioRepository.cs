using System.Security.Cryptography;
using System.Text;
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Models;
using Instapet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Instapet.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DataContext _context;

    public UsuarioRepository(DataContext context)
    {
        _context = context;
    }


    public Usuario? Obter(Guid id)
        => _context.Usuarios
            .Include(p => p.Comentarios)
            .Include(p => p.Curtidas)
            .Include(p => p.PedidosAmizadeEnviados)
            .Include(p => p.PedidosAmizadeRecebidos)
            .Include(p => p.Amigos)
            .Include(p => p.Posts)
            .FirstOrDefault(p => p.Id == id);

    
    public Usuario? GetUser(string username, string password)
    {
        return _context.Usuarios.AsEnumerable().FirstOrDefault(x =>
            string.Equals(x.Email, username, StringComparison.CurrentCultureIgnoreCase)
            && string.Equals(x.HashSenha, Hash(password)));
    }

    public List<Post> ObterPostsPublicos(Guid usuario, int size, int page)
    {
        return _context.Usuarios
            .Where(u => u.Id == usuario)
            .Include(u => u.Posts)
            .ThenInclude(post => post.Usuario)
            .Include(u => u.Posts)
            .ThenInclude(post => post.Curtidas)
            .ThenInclude(curtida => curtida.Usuario)
            .SelectMany(u => u.Posts)
            .Where(post => !post.Privado)
            .OrderByDescending(post => post.Horario)
            .Take(size * (page + 1))
            .ToList();
    }

    public bool ProcuraAmigo(Guid usuario, Guid idAmigo)
    {
        return _context.Usuarios
            .Where(u => u.Id == usuario)
            .SelectMany(u => u.Amigos)
            .Any(u => u.Id == idAmigo);    
    }

    public List<Usuario> ObterAmigos(Guid usuarioId)
    {
        return _context.Usuarios
            .Where(u => u.Id == usuarioId)
            .Include(u => u.Amigos)
            .SelectMany(u => u.Amigos)
            .ToList();
    }

    public List<Post> ObterTodosOsPosts(Guid usuario, int size, int page)
    {
        return _context.Usuarios
            .Where(u => u.Id == usuario)
            .Include(u => u.Posts)
            .ThenInclude(post => post.Usuario)
            .SelectMany(u => u.Posts)
            .OrderByDescending(post => post.Horario)
            .Take(size * (page + 1))
            .ToList();
    }

    public List<PedidoAmizade> ObterPedidosAmizadePendente(Guid idUsuario)
    {
        return _context.Usuarios
            .Where(u => u.Id == idUsuario)
            .Include(u => u.PedidosAmizadeRecebidos)
            .ThenInclude(p => p.Requerente)
            .Include(u => u.PedidosAmizadeRecebidos)
            .ThenInclude(p => p.Requisitado)
            .SelectMany(u => u.PedidosAmizadeRecebidos)
            .ToList();
    }

    public List<Usuario> PesquisarUsuarios(Guid idUsuarioLogado, string texto)
    {
        return _context.Usuarios
            .Where(u => (u.Email.Contains(texto) || u.Nome.Contains(texto)) 
                        && u.Id != idUsuarioLogado 
                        && u.Amigos.All(amigo => amigo.Id != idUsuarioLogado) 
                        && u.PedidosAmizadeEnviados.All(pedido => pedido.Requisitado.Id != idUsuarioLogado)
                        && u.PedidosAmizadeRecebidos.All(pedido => pedido.Requerente.Id != idUsuarioLogado))
            .Include(u => u.Amigos)
            .Include(u => u.PedidosAmizadeEnviados)
            .ThenInclude(pedido => pedido.Requisitado)
            .Include(u => u.PedidosAmizadeRecebidos)
            .ThenInclude(pedido => pedido.Requerente)
            .ToList();
    }

    public bool ExistePedidoAmizadePendente(Guid idUsuario, Guid amigo)
    {
       return _context.Usuarios
            .Where(usuario => usuario.Id == idUsuario)
            .Include(usuario => usuario.PedidosAmizadeRecebidos)
            .ThenInclude(pedido => pedido.Requerente)
            .Include(usuario => usuario.PedidosAmizadeEnviados)
            .ThenInclude(pedido => pedido.Requisitado)
            .Any(usuario => usuario.PedidosAmizadeRecebidos.Any(pedido => pedido.Requerente.Id == amigo) 
                            || usuario.PedidosAmizadeEnviados.Any(pedido => pedido.Requisitado.Id == amigo));
    }

    public List<Post> ObterPosts(Guid usuarioAutenticado, Guid usuarioPosts, int size, int page)
    {
        return (ProcuraAmigo(usuarioPosts, usuarioAutenticado) ? 
            ObterTodosOsPosts(usuarioPosts, size, page) : ObterPostsPublicos(usuarioPosts, size, page));
    }

    public List<Usuario> ObterAmigosFiltrado(Guid idUsuario, string texto)
    {
        return _context.Usuarios
            .Where(u => u.Id == idUsuario)
            .Include(u => u.Amigos)
            .SelectMany(u => u.Amigos)
            .Where(u => u.Nome.Contains(texto) || u.Email.Contains(texto))
            .ToList();
    }

    public void AdicionarUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChangesAsync();
    }

    public bool ExisteEmail(string email)
    {
        var usuarioBuscado = _context.Usuarios.Any(p => p.Email == email);

        return usuarioBuscado;    
    }

    private static string Hash(string input)
    {
        using var md5Hash = MD5.Create();
        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();

        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }

        return sBuilder.ToString();
    }
}

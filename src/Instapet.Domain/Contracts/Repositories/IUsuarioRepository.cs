using Instapet.Domain.Models;

namespace Instapet.Domain.Contracts.Repositories;

public interface IUsuarioRepository
{
    public void AdicionarUsuario(Usuario usuario);

    public bool ExisteEmail(string email);

    Usuario? Obter(Guid id);

    Usuario? GetUser(string loginRequestUsername, string loginRequestPassword);

    List<Post> ObterPostsPublicos(Guid usuario, int size, int page);

    bool ProcuraAmigo(Guid usuario,Guid idAmigo);
    List<Usuario> ObterAmigos(Guid usuarioId);
    List<Post> ObterTodosOsPosts(Guid usuario, int size, int page);

    List<PedidoAmizade> ObterPedidosAmizadePendente(Guid idUsuario);

    List<Usuario> PesquisarUsuarios(Guid idUsuarioLogado, string texto);

    bool ExistePedidoAmizadePendente(Guid idUsuario, Guid amigo);
    List<Post> ObterPosts(Guid usuarioAutenticado, Guid usuarioPosts, int size, int page);

    List<Usuario> ObterAmigosFiltrado(Guid idUsuario, string texto);
}
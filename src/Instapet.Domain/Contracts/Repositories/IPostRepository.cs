using Instapet.Domain.Models;

namespace Instapet.Domain.Contracts.Repositories;

public interface IPostRepository
{
    void Adicionar(Post post);
    bool ExistePost(Guid idPost);

    bool TemAcessoPost(Guid idUsuario, Guid idPost);

    List<Post> ListarPostsHome(Guid idUsuario, int size, int page);
}
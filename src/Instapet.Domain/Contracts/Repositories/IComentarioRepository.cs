using Instapet.Domain.Models;

namespace Instapet.Domain.Contracts.Repositories;

public interface IComentarioRepository
{
    void AdicionarComentario(Comentario comentario);
    List<Comentario> ListarComentarios(Guid idPost);

}
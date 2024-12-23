using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses.Comentario;

namespace Instapet.Application.Contracts;

public interface IComentarioService
{
    RealizarComentarioResponse Comentar(ComentarioRequest request, Guid idPost, Guid guid);
    ListarComentariosResponse ListarComentarios(Guid idAutenticado, Guid idPost);
}
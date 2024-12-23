using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Models;

namespace Instapet.Application.Implementations.Mappers;

public class ComentarioMapper
{
    public static Comentario ToEntity(ComentarioRequest request, Guid idPost, Guid idAutenticado)
    {
        return new Comentario(idAutenticado, idPost, request.Comentario);
    }

    public static ComentarioResponse ToResponse(Comentario comentario)
    {
        var usuario = UsuarioMapper.ToResponse(comentario.Usuario);
        var response = new ComentarioResponse
        {
            Comentario = comentario.Mensagem,
            Horario = comentario.Horario,
            Usuario = usuario
        };
        return response;
    }
}
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Models;
using static Instapet.Application.Implementations.Mappers.UsuarioMapper;

namespace Instapet.Application.Implementations.Mappers;

public class PostMapper
{
    public static Post ToEntity(NovoPostRequest request, Guid guid)
    {
        return new Post(request.UrlFoto, request.Legenda, request.Permissao, guid);
    }

    public static DetalhesPostResponse ToDetalhesResponse(Post post)
    {
        return new DetalhesPostResponse { Privado = post.Privado, Legenda = post.Legenda, Id = post.Id, UrlFoto = post.UrlImagem };
    }

    public static PostHomeResponse ToPostHomeResponse(Post post, Guid idUsuario)
    {
        var usuario = ToResponse(post.Usuario);
        
        var response = new PostHomeResponse
        {
            Id = post.Id,
            Legenda = post.Legenda,
            Url = post.UrlImagem,
            NumeroLikes = post.Curtidas.Count,
            UsuarioResponse = usuario,
            Curtido = post.Curtidas.Any(curtida => curtida.IdUsuario == idUsuario)
        };


        return response;
    }
}
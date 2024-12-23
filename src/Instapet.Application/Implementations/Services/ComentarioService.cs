using Instapet.Application.Contracts;
using Instapet.Application.Implementations.Mappers;
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses.Comentario;
using Instapet.Domain.Validations;

namespace Instapet.Application.Implementations.Services;

public class ComentarioService : IComentarioService
{
    private readonly IComentarioRepository _comentarioRepository;
    private readonly IPostRepository _postRepository;
    private const string PostNaoExiste = "O post nao existe";
    private const string SemAcessoAoPost = "Usuario nao tem acesso ao post";


    public ComentarioService(IComentarioRepository comentarioRepository, IPostRepository postRepository)
    {
        _comentarioRepository = comentarioRepository;
        _postRepository = postRepository;
    }

    public RealizarComentarioResponse Comentar(ComentarioRequest request, Guid idPost, Guid idAutenticado)
    {
        var response = new RealizarComentarioResponse();
        if (!_postRepository.ExistePost(idPost))
        {
            response.AddNotification(new Notification(PostNaoExiste));
            return response;
        }

        if (!_postRepository.TemAcessoPost(idAutenticado, idPost))
        {
            response.AddNotification(new Notification(SemAcessoAoPost));
            return response;
        }

        var novoComentario = ComentarioMapper.ToEntity(request, idPost, idAutenticado);
        _comentarioRepository.AdicionarComentario(novoComentario);

        return new RealizarComentarioResponse { Id = novoComentario.Id };
    }

    public ListarComentariosResponse ListarComentarios(Guid idAutenticado, Guid idPost)
    {

        var response = new ListarComentariosResponse();

        if (!_postRepository.ExistePost(idPost))
        {
            response.AddNotification(new Notification(PostNaoExiste));
            return response;
        }

        if (!_postRepository.TemAcessoPost(idAutenticado, idPost))
        {
            response.AddNotification(new Notification(SemAcessoAoPost));
            return response;
        }
        
        var comentarios = _comentarioRepository.ListarComentarios(idPost);
        response.Comentarios = comentarios.Select(ComentarioMapper.ToResponse)
            .ToList();
        return response;
    }
}
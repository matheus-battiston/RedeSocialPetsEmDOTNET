using Instapet.Application.Contracts;
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Contracts.Responses.Post;
using Instapet.Domain.Models;
using Instapet.Domain.Validations;
using static Instapet.Application.Implementations.Mappers.PostMapper;

namespace Instapet.Application.Implementations.Services;

public class PostService : IPostService
{

    private readonly IPostRepository _postRepository;
    private readonly ICurtidaRepository _curtidaRepository;

    public PostService(IPostRepository postRepository, ICurtidaRepository curtidaRepository)
    {
        _postRepository = postRepository;
        _curtidaRepository = curtidaRepository;
    }

    private const string SemAcessoAoPost = "O post nao existe";
    private const string JaCurtiu = "O usuario ja curtiu o post";
    private const string NaoCurtiu = "O usuario nao curtiu o post";
    

    public NovoPostResponse Postar(NovoPostRequest request, Guid guid)
    {
        var response = new NovoPostResponse();
        var post = ToEntity(request, guid);

        _postRepository.Adicionar(post);

        response.Id = post.Id;

        return response;

    }

    public Notifiable CurtirPost(Guid idUsuario, Guid idPost)
    {
        var response = new Notifiable();

        if (!_postRepository.TemAcessoPost(idUsuario, idPost))
        {
            response.AddNotification(new Notification(SemAcessoAoPost));
            return response;
        }

        if (_curtidaRepository.JaCurtiu(idUsuario, idPost))
        {
            response.AddNotification(new Notification(JaCurtiu));
            return response;
        }

        var curtida = new Curtida
        {
            IdPost = idPost,
            IdUsuario = idUsuario
        };

        _curtidaRepository.Adicionar(curtida);
        
        return response;
    }

    public Notifiable RemoverCurtida(Guid idUsuario, Guid idPost)
    {
        var response = new Notifiable();
        
        if (!_curtidaRepository.JaCurtiu(idUsuario, idPost))
        {
            response.AddNotification(new Notification(NaoCurtiu));
            return response;
        }
        
        _curtidaRepository.RemoverCurtida(idUsuario, idPost);
        return response;
    }

    public List<PostHomeResponse> PostsDaHome(Guid idUsuario, int size, int page)
    {
        var posts = _postRepository.ListarPostsHome(idUsuario, size, page);

        return posts.Select(post => ToPostHomeResponse(post, idUsuario)).ToList();
    }
}
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Contracts.Responses.Post;
using Instapet.Domain.Validations;

namespace Instapet.Application.Contracts;

public interface IPostService
{
    public NovoPostResponse Postar(NovoPostRequest request, Guid guid);

    Notifiable CurtirPost(Guid idUsuario, Guid idPost);

    Notifiable RemoverCurtida(Guid idUsuario, Guid idPost);

    List<PostHomeResponse> PostsDaHome(Guid idUsuario, int size, int page);
}
using Instapet.Domain.Validations;

namespace Instapet.Domain.Contracts.Responses.Post;

public class ListaPostsUsuarioResponse : Notifiable
{
    public List<DetalhesPostResponse> Posts;

    public ListaPostsUsuarioResponse(List<DetalhesPostResponse> posts)
    {
        Posts = posts;
    }
}
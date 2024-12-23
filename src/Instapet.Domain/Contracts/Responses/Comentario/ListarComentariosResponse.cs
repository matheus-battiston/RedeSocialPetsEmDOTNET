using Instapet.Domain.Validations;

namespace Instapet.Domain.Contracts.Responses.Comentario;

public class ListarComentariosResponse : Notifiable
{
    public List<ComentarioResponse> Comentarios { get; set; } = new();
}
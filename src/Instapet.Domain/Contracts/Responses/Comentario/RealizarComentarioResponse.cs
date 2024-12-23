using Instapet.Domain.Validations;

namespace Instapet.Domain.Contracts.Responses.Comentario;

public class RealizarComentarioResponse : Notifiable
{
    public Guid Id { get; set; }
}
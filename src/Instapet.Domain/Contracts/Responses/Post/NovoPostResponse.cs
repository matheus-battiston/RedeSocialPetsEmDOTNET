using Instapet.Domain.Validations;

namespace Instapet.Domain.Contracts.Responses.Post;

public class NovoPostResponse : Notifiable
{
    public Guid Id { get; set; }
}
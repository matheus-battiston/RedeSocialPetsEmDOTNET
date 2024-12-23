using Instapet.Domain.Validations;

namespace Instapet.Domain.Contracts.Responses;

public class UsuarioResponse : Notifiable
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nome { get; set; }
}
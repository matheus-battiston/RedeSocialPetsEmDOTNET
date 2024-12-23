using Instapet.Domain.Validations;

namespace Instapet.Domain.Contracts.Responses;

public class PaginaPerfilResponse : Notifiable
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public bool PerfilPessoal { get; set; }
    public bool Amigo { get; set; }

    public bool PedidoPendente { get; set; }

    public string? urlFotoPerfil { get; set; }
    public List<PostHomeResponse> Posts { get; set; }
}
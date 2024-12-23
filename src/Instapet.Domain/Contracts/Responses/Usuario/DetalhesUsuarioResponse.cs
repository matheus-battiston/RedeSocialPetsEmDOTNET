namespace Instapet.Domain.Contracts.Responses;

public class DetalhesUsuarioResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? UrlFotoPerfil { get; set; }
    public string? Apelido { get; set; }
    
}
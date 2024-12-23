namespace Instapet.Domain.Contracts.Responses;

public class PostHomeResponse
{
    public string? Legenda { get; set; }
    public string Url { get; set; }
    public DetalhesUsuarioResponse UsuarioResponse { get; set; }
    public bool Curtido { get; set; }
    public int NumeroLikes { get; set; }
    public Guid Id { get; set; }
}
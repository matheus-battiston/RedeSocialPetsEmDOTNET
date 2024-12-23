namespace Instapet.Domain.Contracts.Responses;

public class DetalhesPostResponse
{
    public Guid Id { get; set; }
    public string UrlFoto { get; set; }
    public string? Legenda { get; set; }
    public bool Privado { get; set; }
    
}
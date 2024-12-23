namespace Instapet.Domain.Contracts.Responses;

public class ComentarioResponse
{
    public DetalhesUsuarioResponse Usuario { get; set; }
    public DateTime Horario { get; set; }
    public string Comentario { get; set; }
}
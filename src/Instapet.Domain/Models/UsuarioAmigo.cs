namespace Instapet.Domain.Models;

public class UsuarioAmigo : Base
{
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public Guid AmigoId { get; set; }
    public Usuario Amigo { get; set; }
    
}
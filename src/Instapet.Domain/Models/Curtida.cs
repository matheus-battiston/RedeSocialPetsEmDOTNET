namespace Instapet.Domain.Models;

public class Curtida : Base
{
    public Guid IdPost { get; set; }
    public Post Post { get; set; }

    public Guid IdUsuario { get; set; }
    public Usuario Usuario { get; set; }
}
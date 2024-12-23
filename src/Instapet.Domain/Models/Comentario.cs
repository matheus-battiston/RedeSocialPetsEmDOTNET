namespace Instapet.Domain.Models;

public class Comentario : Base
{
    public Comentario(Guid idUsuario, Guid idPost, string mensagem)
    {
        IdUsuario = idUsuario;
        IdPost = idPost;
        Mensagem = mensagem;
    }

    public Guid IdUsuario { get; private set; }
    public Usuario Usuario { get; private set; }

    public Guid IdPost { get; private set; }
    public Post Post { get; private set; }

    public string Mensagem { get; set; }
    public DateTime Horario { get; set; } = DateTime.Now;
}
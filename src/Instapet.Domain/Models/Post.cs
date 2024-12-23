using static System.DateTime;

namespace Instapet.Domain.Models;

public class Post : Base
{
    public Post(string urlImagem, string? legenda, bool privado, Guid idUsuario)
    {
        UrlImagem = urlImagem;
        Legenda = legenda;
        Privado = privado;
        IdUsuario = idUsuario;
        Horario = Now;
    }
    public string UrlImagem { get; private set; }
    public string? Legenda { get; private set; }
    public bool Privado { get; private set; }
    public DateTime Horario { get; private set; }

    public Guid IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    public List<Comentario> Comentarios { get; private set; } = new();
    public List<Curtida> Curtidas { get; private set; } = new();
    
}
using System.Text.Json.Serialization;

namespace Instapet.Domain.Models;

public class Usuario : Base
{
    public Usuario(string nome, string email, DateOnly dataNascimento, string cep, string? urlFotoPerfil, string? apelido, string senha)
    {
        Nome = nome;
        Email = email;
        DataNascimento = dataNascimento;
        CEP = cep;
        UrlFotoPerfil = urlFotoPerfil;
        Apelido = apelido;
        HashSenha = senha;
    }
    
    public Usuario()
    {
        // Default constructor
    }

    public string Nome { get; private set; }
    public string Email { get; private set; }
    public DateOnly DataNascimento { get; private set; }
    public string CEP { get; private set; }
    public string? UrlFotoPerfil { get; private set; }
    public string? Apelido { get; private set; }
    public bool Ativo { get; private set; } = true;

    public string HashSenha { get; private set; }

    
    [JsonIgnore]
    public List<Post> Posts { get; private set; } = new ();
    public List<Usuario> Amigos { get; private set; } = new ();
    public List<PedidoAmizade> PedidosAmizadeRecebidos { get; private set; } = new ();
    public List<PedidoAmizade> PedidosAmizadeEnviados { get; private set; } = new ();
    public List<Curtida> Curtidas { get; private set; } = new();
    public List<Comentario> Comentarios { get; private set; } = new();

}
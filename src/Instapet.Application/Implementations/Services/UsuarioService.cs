using System.Security.Cryptography;
using System.Text;
using Instapet.Application.Contracts;
using Instapet.Application.Implementations.Mappers;
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Contracts.Responses.Login;
using Instapet.Domain.Validations;
using static Instapet.Application.Implementations.Mappers.UsuarioMapper;

namespace Instapet.Application.Implementations.Services;

public class UsuarioService : IUsuarioService
{
    private const string EmailCadastrado = "O email ja foi cadastrado por outro usuario";
    private const string PedidoInvalido = "Pedido de amizade invalido";
    private const string UsuariosNaoSaoAmigos = "Os usuarios informados nao sao amigos";
    private const string UsuariosSaoAmigos = "Os usuarios informados sao amigos";
    private const string ExistePedidoPendente = "Ja existe um pedido de amizade referente a estes usuarios";
    private const string UsuarioNaoExiste = "O Usuario nao existe";


    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPedidoAmizadeRepository _pedidoAmizadeRepository;
    private readonly IUsuarioAmigoRepository _usuarioAmigoRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository, IPedidoAmizadeRepository pedidoAmizadeRepository, IUsuarioAmigoRepository usuarioAmigoRepository)
    {
        _usuarioRepository = usuarioRepository;
        _pedidoAmizadeRepository = pedidoAmizadeRepository;
        _usuarioAmigoRepository = usuarioAmigoRepository;
    }
    
    public UsuarioResponse Cadastrar(CadastrarUsuarioRequest request)
    {
        var response = new UsuarioResponse();
        request.Senha = Hash(request.Senha);
        if (_usuarioRepository.ExisteEmail(request.Email))
        {
            response.AddNotification(new Notification(EmailCadastrado));
            return response;
        }

        var usuario = ToEntity(request);
        
        _usuarioRepository.AdicionarUsuario(usuario);

        response.Email = usuario.Email;
        response.Id = usuario.Id;
        response.Nome = usuario.Nome;
        return response;
    }
    
    public LoginResponse ValidateLogin(LoginRequest loginRequest)
    {
        var usuario = _usuarioRepository.GetUser(loginRequest.Username, loginRequest.Password);

        if (usuario == null)
            return new LoginResponse();

        return new LoginResponse
            { IsAuthenticated = true, Email = usuario.Email, Id = usuario.Id };
    }

    public List<PedidoAmizadeResponse> ListarPedidosAmizade(Guid idAutenticado)
    {
        var pedidosAmizade = _usuarioRepository.ObterPedidosAmizadePendente(idAutenticado);
        return pedidosAmizade.Select(PedidoAmizadeMapper.ToResponse).ToList();

    }

    public Notifiable AdicionarAmigo(Guid idAmigo, Guid idAutenticado)
    {
        var response = new Notifiable();

        if (_usuarioRepository.ProcuraAmigo(idAutenticado, idAmigo))
        {
            response.AddNotification(new Notification(UsuariosSaoAmigos));
            return response;
        }

        if (_usuarioRepository.ExistePedidoAmizadePendente(idAutenticado, idAmigo))
        {
            response.AddNotification(new Notification(ExistePedidoPendente));
            return response;
        }
        
        var pedidoAmizade = PedidoAmizadeMapper.ToEntity(idAmigo, idAutenticado);

        _pedidoAmizadeRepository.Adicionar(pedidoAmizade);
        
        return response;
    }

    public PedidoAmizadeResponse RejeitarPedido(Guid idAutenticado, Guid idPedido)
    {
        var response = new PedidoAmizadeResponse();
        var pedido = _pedidoAmizadeRepository.Obter(idPedido);

        if (pedido is null || pedido.Requisitado.Id != idAutenticado)
        {
            response.AddNotification(new Notification(PedidoInvalido));
            return response;
        }
        
        _pedidoAmizadeRepository.RemoverPedido(pedido);
        
        response.IdPedido = idPedido;
        return response;
    }

    public PedidoAmizadeResponse AceitarPedido(Guid idAutenticado, Guid idPedido)
    {
        var response = new PedidoAmizadeResponse();
        var pedido = _pedidoAmizadeRepository.Obter(idPedido);

        if (pedido is null || pedido.Requisitado.Id != idAutenticado)
        {
            response.AddNotification(new Notification(PedidoInvalido));
            return response;
        }

        var amizadeUm = UsuarioAmigoMapper.ToEntity(pedido.Requisitado, pedido.Requerente);
        var amizadeDois = UsuarioAmigoMapper.ToEntity(pedido.Requerente,pedido.Requisitado);;

        _usuarioAmigoRepository.AdicionarAmigo(amizadeUm, amizadeDois);
        _pedidoAmizadeRepository.RemoverPedido(pedido);
        
        response.IdPedido = idPedido;
        return response;
    }

    public List<DetalhesUsuarioResponse> ListarAmigos(Guid idAutenticado, string texto)
    {
        var amigos = _usuarioRepository.ObterAmigosFiltrado(idAutenticado, texto);

        return amigos.Select(ToResponse).ToList();
    }

    public Notifiable RemoverAmigo(Guid idAutenticado, Guid idAmigo)
    {
        var response = new Notifiable();
        var checarAmigo = _usuarioRepository.ProcuraAmigo(idAutenticado, idAmigo);

        if (!checarAmigo)
        {
            response.AddNotification(new Notification(UsuariosNaoSaoAmigos));
            return response;
        }

        _usuarioAmigoRepository.RemoverAmizade(idAutenticado, idAmigo);

        return response;
    }

    public DetalhesUsuarioResponse Detalhes(Guid idAutenticado)
    {
        var usuario = _usuarioRepository.Obter(idAutenticado);

        return ToResponse(usuario);
    }

    public List<DetalhesUsuarioResponse> BuscarUsuarios(string texto, Guid idAutenticado)
    {
        var usuarios = _usuarioRepository.PesquisarUsuarios(idAutenticado, texto);

        return usuarios.Select(ToResponse).ToList();
    }

    public PaginaPerfilResponse PaginaPerfil(Guid idAutenticado, Guid usuarioId, int size, int page)
    {
        var response = new PaginaPerfilResponse();
        var usuario = _usuarioRepository.Obter(usuarioId);

        if (usuario is null)
        {
            response.AddNotification(new Notification(UsuarioNaoExiste));
            return response;
        }

        bool amizade = usuario.Amigos.Any(amigo => amigo.Id == idAutenticado);
        bool perfilProprio = idAutenticado == usuarioId;
        var posts = _usuarioRepository.ObterPosts(idAutenticado, usuarioId, size, page).Select(post => PostMapper.ToPostHomeResponse(post, idAutenticado)).ToList();

        response.Nome = usuario.Nome;
        response.Id = usuario.Id;
        response.Amigo = amizade;
        response.PerfilPessoal = perfilProprio;
        response.Posts = posts;
        response.urlFotoPerfil = usuario.UrlFotoPerfil;
        response.PedidoPendente = _usuarioRepository.ExistePedidoAmizadePendente(idAutenticado, usuarioId);

        return response;
    }
    
    private static string Hash(string input)
    {
        using var md5Hash = MD5.Create();
        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();

        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }

        return sBuilder.ToString();
    }
}
using Instapet.Application.Contracts;
using Instapet.Application.Implementations.Services;
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Models;
using Moq;
using Xunit;

namespace Instapet.Tests.Services;

public class UsuarioServiceTests
{
    private readonly IUsuarioService _usuarioService;
    private readonly Mock<IUsuarioRepository> _usuarioRepository = new();
    private readonly Mock<IPedidoAmizadeRepository> _pedidoAmizadeRepository = new();
    private readonly Mock<IUsuarioAmigoRepository> _usuarioAmigoRepository = new();
    
    
    public UsuarioServiceTests()
    {
        _usuarioService = new UsuarioService(_usuarioRepository.Object, _pedidoAmizadeRepository.Object, _usuarioAmigoRepository.Object);
    }


    [Fact]
    void deveRetornarNotificacao_QuandoJaExistirUsuarioComEmail()
    {
        var request = new CadastrarUsuarioRequest
        {
            Apelido = "Apelido", 
            Cep = "01234", 
            Email = "email@email.com", 
            Nome = "Nome", 
            Senha = "Minhasenha", 
            UrlFotoPerfil = "Foto", 
            DataNascimento = DateOnly.FromDateTime(new DateTime(1997, 10,10))
        };
        
        _usuarioRepository.Setup(u => u.ExisteEmail(request.Email)).Returns(true);

        var response = _usuarioService.Cadastrar(request);

        Assert.NotEmpty(response.Notifications);
        _usuarioRepository.Verify(u => u.AdicionarUsuario(It.IsAny<Usuario>()), Times.Never);
    }
    
    [Fact]
    void DeveCadastrarSeNaoExistirEmail()
    {
        var request = new CadastrarUsuarioRequest
        {
            Apelido = "Apelido", 
            Cep = "01234", 
            Email = "email@email.com", 
            Nome = "Nome", 
            Senha = "Minhasenha", 
            UrlFotoPerfil = "Foto", 
            DataNascimento = DateOnly.FromDateTime(new DateTime(1997, 10,10))
        };
        
        _usuarioRepository.Setup(u => u.ExisteEmail(request.Email)).Returns(false);

        var response = _usuarioService.Cadastrar(request);

        Assert.Empty(response.Notifications);
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.Nome, response.Nome);
        _usuarioRepository.Verify(u => u.AdicionarUsuario(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    void DeveRetornarNotificacao_QuandoUsuariosSaoAmigos()
    {
        Guid idAmigo = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _usuarioRepository.Setup(u => u.ProcuraAmigo(idUsuario, idAmigo)).Returns(true);

        var response = _usuarioService.AdicionarAmigo(idAmigo, idUsuario);
        
        Assert.NotEmpty(response.Notifications);
        _pedidoAmizadeRepository.Verify(u => u.Adicionar(It.IsAny<PedidoAmizade>()), Times.Never);

    }
    
    
    [Fact]
    void DeveRetornarNotificacao_QuandoUsuariosTemPedidoPendente()
    {
        Guid idAmigo = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _usuarioRepository.Setup(u => u.ProcuraAmigo(idUsuario, idAmigo)).Returns(false);
        _usuarioRepository.Setup(u => u.ExistePedidoAmizadePendente(idUsuario, idAmigo)).Returns(true);

        var response = _usuarioService.AdicionarAmigo(idAmigo, idUsuario);
        
        Assert.NotEmpty(response.Notifications);
        _pedidoAmizadeRepository.Verify(u => u.Adicionar(It.IsAny<PedidoAmizade>()), Times.Never);

    }
    
    [Fact]
    void DeveFazerPedidoDeAmizade()
    {
        Guid idAmigo = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _usuarioRepository.Setup(u => u.ProcuraAmigo(idUsuario, idAmigo)).Returns(false);
        _usuarioRepository.Setup(u => u.ExistePedidoAmizadePendente(idUsuario, idAmigo)).Returns(false);

        var response = _usuarioService.AdicionarAmigo(idAmigo, idUsuario);
        
        Assert.Empty(response.Notifications);
        _pedidoAmizadeRepository.Verify(u => u.Adicionar(It.IsAny<PedidoAmizade>()), Times.Once);
    }

    [Fact]
    void DeveRetornarNotificacao_QuandoPedidoNaoExistir()
    {       
        Guid pedido = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _pedidoAmizadeRepository.Setup(u => u.Obter(pedido)).Returns((PedidoAmizade?)null);

        var response = _usuarioService.RejeitarPedido(idUsuario, pedido);
        
        Assert.NotEmpty(response.Notifications);
        _pedidoAmizadeRepository.Verify(u => u.RemoverPedido(It.IsAny<PedidoAmizade>()), Times.Never);
    }
    
    [Fact]
    void DeveRetornarNotificacao_QuandoPedidoNaoForDoUsuario()
    {
        Usuario usuario = new Usuario();
        Usuario amigo = new Usuario();
        Usuario outroUsuario = new Usuario();
        
        PedidoAmizade pedidoEntity = new PedidoAmizade(amigo.Id, outroUsuario.Id)
        {
            Requerente = amigo,
            Requisitado = outroUsuario
        };

        _pedidoAmizadeRepository.Setup(u => u.Obter(pedidoEntity.Id)).Returns(pedidoEntity);

        var response = _usuarioService.RejeitarPedido(usuario.Id, pedidoEntity.Id);
        
        Assert.NotEmpty(response.Notifications);
        _pedidoAmizadeRepository.Verify(u => u.RemoverPedido(It.IsAny<PedidoAmizade>()), Times.Never);
    }
    
    [Fact]
    void DeveRejeitarPedidoAmizade()
    {
        Usuario usuario = new Usuario();
        Usuario amigo = new Usuario();
        
        PedidoAmizade pedidoEntity = new PedidoAmizade(amigo.Id, usuario.Id)
        {
            Requerente = amigo,
            Requisitado = usuario
        };

        _pedidoAmizadeRepository.Setup(u => u.Obter(pedidoEntity.Id)).Returns(pedidoEntity);

        var response = _usuarioService.RejeitarPedido(usuario.Id, pedidoEntity.Id);
        
        Assert.Empty(response.Notifications);
        _pedidoAmizadeRepository.Verify(u => u.RemoverPedido(pedidoEntity), Times.Once);
    }
    
    [Fact]
    void AceitarPedido_DeveRetornarNotificacao_QuandoPedidoNaoExistir()
    {       
        Guid pedido = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _pedidoAmizadeRepository.Setup(u => u.Obter(pedido)).Returns((PedidoAmizade?)null);

        var response = _usuarioService.AceitarPedido(idUsuario, pedido);
        
        Assert.NotEmpty(response.Notifications);
        _pedidoAmizadeRepository.Verify(u => u.RemoverPedido(It.IsAny<PedidoAmizade>()), Times.Never);
    }
    
    [Fact]
    void AceitarPedido_DeveRetornarNotificacao_QuandoPedidoNaoForDoUsuario()
    {
        Usuario usuario = new Usuario();
        Usuario amigo = new Usuario();
        Usuario outroUsuario = new Usuario();
        
        PedidoAmizade pedidoEntity = new PedidoAmizade(amigo.Id, outroUsuario.Id)
        {
            Requerente = amigo,
            Requisitado = outroUsuario
        };

        _pedidoAmizadeRepository.Setup(u => u.Obter(pedidoEntity.Id)).Returns(pedidoEntity);

        var response = _usuarioService.AceitarPedido(usuario.Id, pedidoEntity.Id);
        
        Assert.NotEmpty(response.Notifications);
        _usuarioAmigoRepository.Verify(u => u.AdicionarAmigo(It.IsAny<UsuarioAmigo>(), It.IsAny<UsuarioAmigo>()), Times.Never);
        _pedidoAmizadeRepository.Verify(u => u.RemoverPedido(It.IsAny<PedidoAmizade>()), Times.Never);
    }
    
    [Fact]
    void DeveAceitarPedidoAmizade()
    {
        Usuario usuario = new Usuario();
        Usuario amigo = new Usuario();
        
        PedidoAmizade pedidoEntity = new PedidoAmizade(amigo.Id, usuario.Id)
        {
            Requerente = amigo,
            Requisitado = usuario
        };

        _pedidoAmizadeRepository.Setup(u => u.Obter(pedidoEntity.Id)).Returns(pedidoEntity);

        var response = _usuarioService.AceitarPedido(usuario.Id, pedidoEntity.Id);
        
        Assert.Empty(response.Notifications);
        _usuarioAmigoRepository.Verify(u => u.AdicionarAmigo(It.IsAny<UsuarioAmigo>(), It.IsAny<UsuarioAmigo>()), Times.Once);
        _pedidoAmizadeRepository.Verify(u => u.RemoverPedido(pedidoEntity), Times.Once);
    }

    [Fact]
    void RemoverAmigo_DeveRetornarNotificacao_QuandoNaoForemAmigos()
    {
        Guid idUsuario = Guid.NewGuid();
        Guid idAmigo = Guid.NewGuid();
        
        
        _usuarioRepository.Setup(u => u.ProcuraAmigo(idUsuario, idAmigo)).Returns(false);

        var response = _usuarioService.RemoverAmigo(idUsuario, idAmigo);
        
        Assert.NotEmpty(response.Notifications);
        _usuarioAmigoRepository.Verify(u => u.RemoverAmizade(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);


    }
    
    [Fact]
    void RemoverAmigo_DeveRemoverAmizade()
    {
        Guid idUsuario = Guid.NewGuid();
        Guid idAmigo = Guid.NewGuid();
        
        
        _usuarioRepository.Setup(u => u.ProcuraAmigo(idUsuario, idAmigo)).Returns(true);

        var response = _usuarioService.RemoverAmigo(idUsuario, idAmigo);
        
        Assert.Empty(response.Notifications);
        _usuarioAmigoRepository.Verify(u => u.RemoverAmizade(idUsuario, idAmigo), Times.Once);
    }

    [Fact]
    void Detalhes_DeveDetalharOUsuario()
    {
        var usuario = new Usuario("nome", "email", DateOnly.FromDateTime(new DateTime(2020, 10, 10)), "cep", "url", "apelido", "senha"  );
        
        _usuarioRepository.Setup(u => u.Obter(usuario.Id)).Returns(usuario);

        var response = _usuarioService.Detalhes(usuario.Id);
        
        Assert.Equal(usuario.Email,response.Email);
        Assert.Equal(usuario.Nome,response.Nome);
        Assert.Equal(usuario.Apelido,response.Apelido);
        Assert.Equal(usuario.UrlFotoPerfil,response.UrlFotoPerfil);
        Assert.Equal(usuario.Id,response.Id);
    }
    
    [Fact]
    void PaginaPerfil_DeveRetornarNotificacao_QuandoUsuarioNaoExistir()
    {       
        Guid idAutenticado = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        int size = 2;
        int page = 2;
        
        _usuarioRepository.Setup(u => u.Obter(idUsuario)).Returns((Usuario?)null);

        var response = _usuarioService.PaginaPerfil(idAutenticado, idUsuario, size, page);
        
        Assert.NotEmpty(response.Notifications);
        _usuarioRepository.Verify(u => u.ObterPosts(idAutenticado, idUsuario, size, page), Times.Never);
        _usuarioRepository.Verify(u => u.ExistePedidoAmizadePendente(idAutenticado, idUsuario), Times.Never);

    }
}
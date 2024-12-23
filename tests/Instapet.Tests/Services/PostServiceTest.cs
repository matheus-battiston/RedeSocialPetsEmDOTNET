using Instapet.Application.Contracts;
using Instapet.Application.Implementations.Services;
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Models;
using Moq;
using Xunit;

namespace Instapet.Tests.Services;

public class PostServiceTest
{
    private readonly Mock<IPostRepository> _postRepository = new();
    private readonly Mock<ICurtidaRepository> _curtidaRepository = new();
    private readonly IPostService _postService;

    public PostServiceTest()
    {
        _postService = new PostService(_postRepository.Object, _curtidaRepository.Object);
    }

    [Fact]
    void Postar_DeveFazerUmPost()
    {
        var usuario = Guid.NewGuid();
        var request = new NovoPostRequest{Legenda = "Legenda", Permissao = false, UrlFoto = "URL DA FOTO"};

        var response = _postService.Postar(request, usuario);
        
        Assert.Empty(response.Notifications);
        _postRepository.Verify(u => u.Adicionar(It.IsAny<Post>()), Times.Once);

    }

    [Fact]
    void CurtirPost_DeveTerNotificacao_QuandoNaoTemAcesso()
    {
        Guid idPost = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _postRepository.Setup(u => u.TemAcessoPost(idUsuario, idPost)).Returns(false);

        var response = _postService.CurtirPost(idUsuario, idPost);
        Assert.NotEmpty(response.Notifications);
        _curtidaRepository.Verify(u => u.Adicionar(It.IsAny<Curtida>()), Times.Never);
    }
    
    [Fact]
    void CurtirPost_DeveTerNotificacao_QuandoJaCurtiu()
    {
        Guid idPost = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _postRepository.Setup(u => u.TemAcessoPost(idUsuario, idPost)).Returns(true);
        _curtidaRepository.Setup(u => u.JaCurtiu(idUsuario, idPost)).Returns(true);

        var response = _postService.CurtirPost(idUsuario, idPost);
        Assert.NotEmpty(response.Notifications);
        _curtidaRepository.Verify(u => u.Adicionar(It.IsAny<Curtida>()), Times.Never);
    }
    
    [Fact]
    void CurtirPost_NaoDeveTerNotificacao_QuandoPuderCurtir()
    {
        Guid idPost = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _postRepository.Setup(u => u.TemAcessoPost(idUsuario, idPost)).Returns(true);
        _curtidaRepository.Setup(u => u.JaCurtiu(idUsuario, idPost)).Returns(false);

        var response = _postService.CurtirPost(idUsuario, idPost);
        Assert.Empty(response.Notifications);
        _curtidaRepository.Verify(u => u.Adicionar(It.IsAny<Curtida>()), Times.Once);
    }
    
    [Fact]
    void CurtirPost_DeveTerNotificacao_QuandoNaoTemCurtida()
    {
        Guid idPost = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _curtidaRepository.Setup(u => u.JaCurtiu(idUsuario, idPost)).Returns(false);

        var response = _postService.RemoverCurtida(idUsuario, idPost);
        Assert.NotEmpty(response.Notifications);
        _curtidaRepository.Verify(u => u.RemoverCurtida(idUsuario, idPost), Times.Never);
    }
    
    [Fact]
    void CurtirPost_NaoDeveTerNotificacao_QuandoPuderRemoverCurtida()
    {
        Guid idPost = Guid.NewGuid();
        Guid idUsuario = Guid.NewGuid();
        
        _curtidaRepository.Setup(u => u.JaCurtiu(idUsuario, idPost)).Returns(true);

        var response = _postService.RemoverCurtida(idUsuario, idPost);
        Assert.Empty(response.Notifications);
        _curtidaRepository.Verify(u => u.RemoverCurtida(idUsuario, idPost), Times.Once);
    }
    
}
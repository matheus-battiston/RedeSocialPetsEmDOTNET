using Instapet.Application.Contracts;
using Instapet.Application.Implementations.Mappers;
using Instapet.Application.Implementations.Services;
using Instapet.Domain.Contracts.Repositories;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Models;
using Moq;
using Xunit;

namespace Instapet.Tests.Services;

public class ComentarioServiceTest
{
    private readonly IComentarioService _comentarioService;
    private readonly Mock<IComentarioRepository> _comentarioRepository = new();
    private readonly Mock<IPostRepository> _postRepository = new();
    
    public ComentarioServiceTest()
    {
        _comentarioService = new ComentarioService(_comentarioRepository.Object, _postRepository.Object);
    }

    [Fact]
    void Comentar_DeveRetornarNotificacao_QuandoNaoExiste()
    {
        Guid usuario = Guid.NewGuid();
        Guid idPost = Guid.NewGuid();
        ComentarioRequest request = new ComentarioRequest{Comentario = "Comentario"};
        
        _postRepository.Setup(u => u.ExistePost(idPost)).Returns(false);

        var response = _comentarioService.Comentar(request, idPost, usuario);
        
        Assert.NotEmpty(response.Notifications);
        _comentarioRepository.Verify(u => u.AdicionarComentario(It.IsAny<Comentario>()), Times.Never);

    }
    
    [Fact]
    void Comentar_DeveRetornarNotificacao_QuandoNaoTemAcesso()
    {
        Guid usuario = Guid.NewGuid();
        Guid idPost = Guid.NewGuid();
        ComentarioRequest request = new ComentarioRequest{Comentario = "Comentario"};
        
        _postRepository.Setup(u => u.ExistePost(idPost)).Returns(true);
        _postRepository.Setup(u => u.TemAcessoPost(usuario, idPost)).Returns(false);
        
        var response = _comentarioService.Comentar(request, idPost, usuario);
        
        Assert.NotEmpty(response.Notifications);
        _comentarioRepository.Verify(u => u.AdicionarComentario(It.IsAny<Comentario>()), Times.Never);

    }
    
    [Fact]
    void Comentar_DeveFazerUmComentario()
    {
        Guid usuario = Guid.NewGuid();
        Guid idPost = Guid.NewGuid();
        ComentarioRequest request = new ComentarioRequest{Comentario = "Comentario"};

        _postRepository.Setup(u => u.ExistePost(idPost)).Returns(true);
        _postRepository.Setup(u => u.TemAcessoPost(usuario, idPost)).Returns(true);
        
        var response = _comentarioService.Comentar(request, idPost, usuario);
        
        Assert.Empty(response.Notifications);
        _comentarioRepository.Verify(u => u.AdicionarComentario(It.IsAny<Comentario>()), Times.Once);

    }
    
    [Fact]
    void ListarComentarios_DeveRetornarNotificacao_QuandoNaoExiste()
    {
        Guid usuario = Guid.NewGuid();
        Guid idPost = Guid.NewGuid();
        
        _postRepository.Setup(u => u.ExistePost(idPost)).Returns(false);

        var response = _comentarioService.ListarComentarios(usuario, idPost);
        
        Assert.NotEmpty(response.Notifications);
        _comentarioRepository.Verify(u => u.ListarComentarios(idPost), Times.Never);

    }
    
    [Fact]
    void ListarComentarios_DeveRetornarNotificacao_QuandoNaoTemAceso()
    {
        Guid usuario = Guid.NewGuid();
        Guid idPost = Guid.NewGuid();
        
        _postRepository.Setup(u => u.ExistePost(idPost)).Returns(true);
        _postRepository.Setup(u => u.TemAcessoPost(usuario, idPost)).Returns(false);
        
        var response = _comentarioService.ListarComentarios(usuario, idPost);
        
        Assert.NotEmpty(response.Notifications);
        _comentarioRepository.Verify(u => u.ListarComentarios(idPost), Times.Never);

    }
    
    [Fact]
    void ListarComentarios_DeveRetornarListaDeComentarios()
    {
        Guid usuario = Guid.NewGuid();
        Guid idPost = Guid.NewGuid();

        _postRepository.Setup(u => u.ExistePost(idPost)).Returns(true);
        _postRepository.Setup(u => u.TemAcessoPost(usuario, idPost)).Returns(true);
        _comentarioRepository.Setup(u => u.ListarComentarios(idPost)).Returns(new List<Comentario>());

        
        var response = _comentarioService.ListarComentarios(usuario, idPost);
        
        Assert.Empty(response.Notifications);
        _comentarioRepository.Verify(u => u.ListarComentarios(idPost), Times.Once);

    }
}
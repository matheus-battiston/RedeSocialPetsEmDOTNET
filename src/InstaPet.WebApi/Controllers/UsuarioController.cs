using Instapet.Application.Contracts;
using Instapet.Domain.Contracts;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Contracts.Responses.Post;
using Instapet.Domain.Validations;
using InstaPet.WebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstaPet.WebApi.Controllers;

[Route("/usuarios")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly TokenService _tokenService;

    public UsuarioController(IUsuarioService usuarioService, TokenService tokenService)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }
    
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public Task<ActionResult<UsuarioResponse>> Cadastrar([FromBody] CadastrarUsuarioRequest request)
    {
        var response = _usuarioService.Cadastrar(request);
        
        if (!response.IsValid())
            return Task.FromResult<ActionResult<UsuarioResponse>>(BadRequest(new ErrorResponse(response.Notifications)));

        return Task.FromResult<ActionResult<UsuarioResponse>>(Ok(response));
    }
    
    [HttpPost]
    [Route("/login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var loginResponse = _usuarioService.ValidateLogin(loginRequest);

        if (!loginResponse.IsAuthenticated)
            return BadRequest("Usuário e/ou senha inválidos");

        var token = _tokenService.GenerateToken(loginResponse);

        return Ok( new
        {
            user = loginResponse,
            token
        });
    }
    
    [HttpGet]
    [Route("pagina-usuario/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginaPerfilResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<PaginaPerfilResponse> PaginaPerfil([FromRoute] Guid id, [FromQuery] string size, [FromQuery] string page)
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        var response = _usuarioService.PaginaPerfil(idAutenticado, id, int.Parse(size), int.Parse(page));
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok(response);
        
    }
    
    [HttpGet]
    [Route("pedidos-amizade-pendente")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PedidoAmizadeResponse>))]
    public ActionResult<List<PedidoAmizadeResponse>> PedidosDeAmizadePendente()
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        return _usuarioService.ListarPedidosAmizade(idAutenticado);

    }
    
    [HttpPost]
    [Route("nova-amizade/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Notifiable))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<Notifiable> NovoPedidoAmizade([FromRoute] Guid id)
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        var response = _usuarioService.AdicionarAmigo(id, idAutenticado);
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok();

    }
    
    
    [HttpGet]
    [Route("listar-amigos")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DetalhesUsuarioResponse>))]
    public ActionResult<List<DetalhesUsuarioResponse>> ListarAmigos([FromQuery] string texto = "")
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        return _usuarioService.ListarAmigos(idAutenticado, texto);

    }
    
    [HttpPut]
    [Route("aceitar-amizade/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoAmizadeResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<PedidoAmizadeResponse> AceitarAmizade([FromRoute] Guid id)
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        PedidoAmizadeResponse response = _usuarioService.AceitarPedido(idAutenticado, id);
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok(response);

    }
    
    
    [HttpPut]
    [Route("desfazer-amizade/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Notifiable))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<Notifiable> RemoverAmigo([FromRoute] Guid id)
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        Notifiable response = _usuarioService.RemoverAmigo(idAutenticado, id);
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok();

    }
    
    [HttpPut]
    [Route("recusar-amizade/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PedidoAmizadeResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<PedidoAmizadeResponse> RecusarAmizade([FromRoute] Guid id)
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        PedidoAmizadeResponse response = _usuarioService.RejeitarPedido(idAutenticado, id);
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok(response);

    }
    
    [HttpGet]
    [Route("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetalhesUsuarioResponse))]
    public ActionResult<DetalhesUsuarioResponse> Detalhes()
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        return _usuarioService.Detalhes(idAutenticado);

    }
    
    [HttpGet]
    [Route("buscar-usuarios")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DetalhesUsuarioResponse>))]
    public ActionResult<List<DetalhesUsuarioResponse>> BuscarUsuarios([FromQuery] string texto = "")
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        return _usuarioService.BuscarUsuarios(texto, idAutenticado);

    }
}
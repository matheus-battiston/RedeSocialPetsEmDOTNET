using Instapet.Application.Contracts;
using Instapet.Domain.Contracts;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses.Comentario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstaPet.WebApi.Controllers;

[ApiController]
[Route("/comentarios")]
public class ComentarioController : Controller
{
    private readonly IComentarioService _comentarioService;

    public ComentarioController(IComentarioService comentarioService)
    {
        _comentarioService = comentarioService;
    }


    [HttpPost]
    [Route("comentar/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RealizarComentarioResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<RealizarComentarioResponse> Comentar([FromBody] ComentarioRequest request, [FromRoute] Guid id)
    {
        string? stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        var response = _comentarioService.Comentar(request, id,idAutenticado);

        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok();
    }
    
    [HttpGet]
    [Route("listar/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListarComentariosResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<ListarComentariosResponse> ListarComentariosPost([FromRoute] Guid id)
    {
        
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        var response = _comentarioService.ListarComentarios(idAutenticado, id);
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok(response.Comentarios);
        
    }
}
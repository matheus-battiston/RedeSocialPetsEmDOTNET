using Instapet.Application.Contracts;
using Instapet.Domain.Contracts;
using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstaPet.WebApi.Controllers;

[Route("/posts")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }


    [HttpPost]
    [Route("curtir/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<Notifiable> Curtir([FromRoute] Guid id)
    {
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        var response = _postService.CurtirPost(idAutenticado, id);
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok();
    }
    
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public IActionResult Postar([FromBody] NovoPostRequest request)
    {
        string? stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var id = Guid.Parse(stringId);

        var response = _postService.Postar(request, id);

        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok();
    }
    
    [HttpPost]
    [Route("remover-curtida/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public ActionResult<Notifiable> RemoverCurtida([FromRoute] Guid id)
    {
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        var response = _postService.RemoverCurtida(idAutenticado, id);
        
        if (!response.IsValid())
            return BadRequest(new ErrorResponse(response.Notifications));

        return Ok();
    }
    
    
    [HttpGet]
    [Route("listar-posts-home")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PostHomeResponse>))]
    public ActionResult<List<PostHomeResponse>> PostsHome([FromQuery] string size, [FromQuery] string page)
    {
        var stringId = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"))?.Value;
        var idAutenticado = Guid.Parse(stringId);

        var response = _postService.PostsDaHome(idAutenticado, int.Parse(size), int.Parse(page));
        

        return Ok(response);
    }
}
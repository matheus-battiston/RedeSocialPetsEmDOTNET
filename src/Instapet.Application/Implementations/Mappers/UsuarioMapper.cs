using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Models;

namespace Instapet.Application.Implementations.Mappers;

public class UsuarioMapper
{
    public static Usuario ToEntity(CadastrarUsuarioRequest request)
    {
        var usuario = new Usuario(request.Nome, 
            request.Email, 
            request.DataNascimento, 
            request.Cep, 
            request.UrlFotoPerfil, 
            request.Apelido, 
            request.Senha);
        
        return usuario;
    }

    public static DetalhesUsuarioResponse ToResponse(Usuario entityRequerente)
    {
        return new DetalhesUsuarioResponse
        {
            Apelido = entityRequerente.Apelido,
            Email = entityRequerente.Email,
            Id = entityRequerente.Id,
            Nome = entityRequerente.Nome,
            UrlFotoPerfil = entityRequerente.UrlFotoPerfil
        };
    }
}
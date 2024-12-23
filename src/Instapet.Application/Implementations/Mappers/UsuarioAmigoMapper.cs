using Instapet.Domain.Models;

namespace Instapet.Application.Implementations.Mappers;

public class UsuarioAmigoMapper
{
    public static UsuarioAmigo ToEntity(Usuario pedidoRequisitado, Usuario pedidoRequerente)
    {
        var amizade = new UsuarioAmigo();
        amizade.Usuario = pedidoRequisitado;
        amizade.Amigo = pedidoRequerente;

        return amizade;
    }
}
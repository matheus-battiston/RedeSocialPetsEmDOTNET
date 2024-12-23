using Instapet.Domain.Contracts.Requests;
using Instapet.Domain.Contracts.Responses;
using Instapet.Domain.Contracts.Responses.Login;
using Instapet.Domain.Contracts.Responses.Post;
using Instapet.Domain.Validations;

namespace Instapet.Application.Contracts;

public interface IUsuarioService
{
    public UsuarioResponse Cadastrar(CadastrarUsuarioRequest request);
    LoginResponse ValidateLogin(LoginRequest loginRequest);
    List<PedidoAmizadeResponse> ListarPedidosAmizade(Guid idAutenticado);

    Notifiable AdicionarAmigo(Guid idAmigo, Guid idAutenticado);

    PedidoAmizadeResponse RejeitarPedido(Guid idAutenticado, Guid idPedido);
    PedidoAmizadeResponse AceitarPedido(Guid idAutenticado, Guid idPedido);

    List<DetalhesUsuarioResponse> ListarAmigos(Guid idAutenticado, string texto);

    Notifiable RemoverAmigo(Guid idAutenticado, Guid idAmigo);

    DetalhesUsuarioResponse Detalhes(Guid idAutenticado);

    List<DetalhesUsuarioResponse> BuscarUsuarios(string texto, Guid idAutenticado);

    PaginaPerfilResponse PaginaPerfil(Guid idAutenticado, Guid usuarioId, int size, int page);
}
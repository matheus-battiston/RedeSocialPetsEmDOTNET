using Instapet.Domain.Models;

namespace Instapet.Domain.Contracts.Repositories;

public interface IUsuarioAmigoRepository
{
    void AdicionarAmigo(UsuarioAmigo amizadeUm, UsuarioAmigo amizadeDois);
    void RemoverAmizade(Guid idAutenticado, Guid idAmigo);
}
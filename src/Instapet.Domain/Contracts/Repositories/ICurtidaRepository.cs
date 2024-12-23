using Instapet.Domain.Models;

namespace Instapet.Domain.Contracts.Repositories;

public interface ICurtidaRepository
{
    void Adicionar(Curtida curtida);

    bool JaCurtiu(Guid idUsuario, Guid idPost);

    void RemoverCurtida(Guid idUsuario, Guid idPost);
}
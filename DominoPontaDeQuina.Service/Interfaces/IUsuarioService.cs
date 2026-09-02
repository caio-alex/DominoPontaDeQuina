using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Service.Interfaces;

public interface IUsuarioService
{
    Task<Usuario> CadastrarAsync(string nome, string email, string senha);
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task<List<Usuario>> ListarAsync();
}

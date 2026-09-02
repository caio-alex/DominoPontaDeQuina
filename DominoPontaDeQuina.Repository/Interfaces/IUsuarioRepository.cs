using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Repository.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task<List<Usuario>> ListarAsync();
    Task AdicionarAsync(Usuario usuario);
}

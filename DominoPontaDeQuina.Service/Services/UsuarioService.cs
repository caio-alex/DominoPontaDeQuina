using System.Security.Cryptography;
using System.Text;
using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Service.Interfaces;

namespace DominoPontaDeQuina.Service.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario> CadastrarAsync(string nome, string email, string senha)
    {
        var existente = await _usuarioRepository.ObterPorEmailAsync(email);
        if (existente is not null)
        {
            throw new InvalidOperationException($"Já existe um usuário cadastrado com o e-mail '{email}'.");
        }

        var usuario = new Usuario
        {
            Nome = nome,
            Email = email,
            HashSenha = HashSenha(senha)
        };

        await _usuarioRepository.AdicionarAsync(usuario);
        return usuario;
    }

    public Task<Usuario?> ObterPorIdAsync(Guid id) =>
        _usuarioRepository.ObterPorIdAsync(id);

    public Task<List<Usuario>> ListarAsync() =>
        _usuarioRepository.ListarAsync();

    private static string HashSenha(string senha)
    {
        var bytes = Encoding.UTF8.GetBytes(senha);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash);
    }
}

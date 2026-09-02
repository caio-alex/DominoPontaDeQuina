using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using DominoPontaDeQuina.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DominoPontaDeQuina.Repository.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DominoDbContext _context;

    public UsuarioRepository(DominoDbContext context)
    {
        _context = context;
    }

    public Task<Usuario?> ObterPorIdAsync(Guid id) =>
        _context.Usuarios
            .Include(u => u.Jogadores)
            .FirstOrDefaultAsync(u => u.Id == id);

    public Task<Usuario?> ObterPorEmailAsync(string email) =>
        _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);

    public Task<List<Usuario>> ListarAsync() =>
        _context.Usuarios
            .OrderBy(u => u.Nome)
            .ToListAsync();

    public async Task AdicionarAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}
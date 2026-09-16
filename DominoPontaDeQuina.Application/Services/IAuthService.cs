namespace DominoPontaDeQuina.Application.Services
{
    public interface IAuthService
    {
        // O método recebe os dados do usuário e retorna o token JWT em formato string
        string GerarToken(string usuarioId, string nome);
    }
}
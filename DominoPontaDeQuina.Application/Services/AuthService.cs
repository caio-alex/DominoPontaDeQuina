using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using DominoPontaDeQuina.Application.Services;

namespace DominoPontaDeQuina.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(string usuarioId, string nome)
        {
            // 1. Manipulador responsável por criar o token
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Recupera a chave secreta (certifique-se de ter essa chave no appsettings.json)
            // A chave deve ter pelo menos 16 a 32 caracteres para usar algoritmos seguros (como HMAC SHA-256)
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "");

            // 3. Define o Payload, contendo os dados do usuário (claims)[cite: 2]
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioId),
                new Claim(ClaimTypes.Name, nome)
                // Você pode adicionar outras roles/permissões aqui
            });

            // 4. Configura o descritor do token, unindo o Header, o Payload e definindo a Signature[cite: 2]
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(2), // Define a validade do token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature) // Algoritmo de assinatura
            };

            // 5. Gera e converte o token para uma string
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
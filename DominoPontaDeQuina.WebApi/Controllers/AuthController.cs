using Microsoft.AspNetCore.Mvc;
using DominoPontaDeQuina.Application.Services;

namespace DominoPontaDeQuina.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            // Em um cenário real, você receberia um DTO com Login/Senha e validaria no banco de dados.
            // Para testarmos a geração do token agora, vamos usar dados fictícios simulando um login de sucesso.
            string idMock = "1";
            string nomeMock = "Caio";

            // Chama o serviço para gerar a string do token
            var token = _authService.GerarToken(idMock, nomeMock);

            // Retorna o Status 200 OK com o token no formato JSON[cite: 2]
            return Ok(new { Token = token });
        }
    }
}
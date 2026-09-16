// Exemplo genérico dentro da action de Login no AuthController
var token = _authService.GerarToken(usuarioEncontrado.Id.ToString(), usuarioEncontrado.Nome);
return Ok(new { Token = token }); // Retorna HTTP 200 (Sucesso)[cite: 2]
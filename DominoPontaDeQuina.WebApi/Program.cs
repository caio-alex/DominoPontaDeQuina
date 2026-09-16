using DominoPontaDeQuina.Core;
using DominoPontaDeQuina.Domain;
using DominoPontaDeQuina.Repository;
using DominoPontaDeQuina.Application;
using DominoPontaDeQuina.Infrastructure;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// --- Serviços do ASP.NET Core (Controllers + Swagger) ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- Camada de Application ---
// Registra o IPartidaService (e qualquer outro serviço de aplicação) no container de DI.
builder.Services.AddDominoApplication();

// --- Camada de Infrastructure ---
// AddDominoInfrastructure só sabe registrar o DbContext e os repositórios; quem decide
// QUAL banco usar é a Web API, passando a configuração do provedor aqui.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "A connection string 'DefaultConnection' não foi configurada em appsettings.json.");

builder.Services.AddDominoInfrastructure(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// --- Pipeline HTTP ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuaChaveSecretaSuperSeguraAqui")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

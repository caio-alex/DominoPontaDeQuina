using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Se o seu DbContext estiver em outro namespace, o VS Code pode pedir para ajustar este using:
using DominoPontaDeQuina.Infrastructure.Persistence; 

var builder = WebApplication.CreateBuilder(args);

// ==============================================================
// 1. ADICIONAR SERVIÇOS AO CONTÊINER (Tudo que usa "builder.Services")
// ==============================================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1.1 Configuração do Banco de Dados (Entity Framework)
builder.Services.AddDbContext<DominoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1.2 Configuração da Autenticação via JWT
var secretKey = builder.Configuration["JwtSettings:SecretKey"] ?? "chave_super_secreta_de_desenvolvimento_123456789";
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => 
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Nota: Se você criou injeções separadas nas outras camadas, coloque as chamadas aqui. 
// Exemplo: builder.Services.AddInfrastructure(); ou builder.Services.AddApplication();

// ==============================================================
// 2. BUILD (Bloqueia o contêiner - Nada mais pode ser adicionado)
// ==============================================================
var app = builder.Build();

// ==============================================================
// 3. PIPELINE DE REQUISIÇÕES (Tudo que usa "app.Use...")
// ==============================================================

// Swagger (Interface para testar a API)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// A ordem destas duas linhas é obrigatória no .NET:
app.UseAuthentication(); // 1º: Descobre QUEM é o usuário pelo Token
app.UseAuthorization();  // 2º: Verifica se ele tem PERMISSÃO para acessar a rota

app.MapControllers();

app.Run();
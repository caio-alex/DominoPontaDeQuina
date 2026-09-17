using DominoPontaDeQuina.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DominoPontaDeQuina.Application;

/// <summary>Registra os serviços da camada de aplicação no contêiner de DI.</summary>
public static class DependencyInjection
{
    /// <summary>Adiciona os serviços de aplicação.</summary>
    public static IServiceCollection AddDominoApplication(this IServiceCollection services)
    {
        // Aqui registramos APENAS as regras de negócio (Services)
        services.AddScoped<IPartidaService, PartidaService>();

        // Não se esqueça de registrar o AuthService que construímos hoje mais cedo!
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
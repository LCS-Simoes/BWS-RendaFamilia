using BWS.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;


namespace BWS.Application.Helper
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationUser(this IServiceCollection services)
        {
            services.AddScoped<ClientesUseCase>();
          
            return services;
        }
    }
}

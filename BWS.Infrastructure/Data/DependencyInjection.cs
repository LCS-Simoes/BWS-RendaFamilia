using BWS.Domain.Interfaces;
using BWS.Infrastructure.Data.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BWS.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<bwsDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IClientes, ClientesRepositorio>();

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tecnicos.Data.Context;

namespace Tecnicos.Data.DI
{
    public static class DbContextRegistar
    {
        public static IServiceCollection RegisterDbContextFactory(this IServiceCollection services)
        {
            services.AddDbContextFactory<ClientesContext>(options =>
                options.UseSqlServer("Name=SqlConStr",
                    sqlOptions => sqlOptions.MigrationsAssembly("RegistroClienteApi")
                )
            );
            return services;
        }
    }
}

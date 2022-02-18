using DDDBasico.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



public static class DepencyInjection
{
    public static void ConfigureStartup(this IServiceCollection services, IConfiguration config)
    {

        ConfigureDB(services, config);

    }

    public static void ConfigureDB(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<ApplicationDbContext>(options =>{options.UseSqlite(config.GetConnectionString("DefaultConnection"));});

    }

}

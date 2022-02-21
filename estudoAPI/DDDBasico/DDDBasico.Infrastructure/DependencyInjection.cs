using DDDBasico.Domain.Interfaces;
using DDDBasico.Infrastructure.Context;
using DDDBasico.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



public static class DepencyInjection
{
    public static void ConfigureStartup(this IServiceCollection services, IConfiguration config)
    {

        ConfigureDB(services, config);
        services.AddTransient<IRepositoryUser, RepositoryUser>();
        services.AddMediatR(AppDomain.CurrentDomain.Load("DDDBasico.Application"));

    }

    public static void ConfigureDB(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<ApplicationDbContext>(options =>{options.UseSqlite(config.GetConnectionString("DefaultConnection"));});

    }

}

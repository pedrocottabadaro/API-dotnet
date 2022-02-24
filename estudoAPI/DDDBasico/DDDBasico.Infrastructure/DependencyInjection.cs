using DDDBasico.Domain.Interfaces;
using DDDBasico.Infrastructure.Context;
using DDDBasico.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class DepencyInjection
{
    public static void ConfigureStartup(this IServiceCollection services, IConfiguration config)
    {

        ConfigureDB(services, config);
        ConfigureToken(services, config);
        services.AddTransient<IRepositoryUser, RepositoryUser>();
        services.AddTransient<IRepositoryLog, RepositoryLog>();
        services.AddMediatR(AppDomain.CurrentDomain.Load("DDDBasico.Application"));

    }

    public static void ConfigureDB(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<ApplicationDbContext>(options =>{options.UseSqlite(config.GetConnectionString("DefaultConnection"),b=>b.MigrationsAssembly("DDDBasico.Infrastructure"));});

    }


    public static void ConfigureToken(this IServiceCollection services, IConfiguration config)
    {

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        });
    }

}

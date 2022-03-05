using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using DDDBasico.Infrastructure.Context;
using DDDBasico.Infrastructure.Repositories;
using DDDBasico.Infrastructure.Services.JWT;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
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
        services.AddTransient<IRepositoryLog, RepositoryLog>();
        services.AddTransient<IRepositoryUser, RepositoryUser>();
        services.AddHttpContextAccessor();
        services.AddScoped<ITokenService, TokenService>();

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

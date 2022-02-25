using DDDBasico.Application.DTO;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Command.Auth
{
    public record LoginAuthCommand(String Password, String email) : IRequest<JwtDTO>;

    public class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, JwtDTO>
    {

        private readonly IRepositoryUser _repository;
        private readonly ITokenService _tokenService;

        public LoginAuthCommandHandler(IRepositoryUser repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService= tokenService;
        }

        public async Task<JwtDTO> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = _repository.checkUserExists(request.email);
                if (existingUser == null) return null;
                using var hmac = new HMACSHA512(existingUser.PasswordSalt);
                var hashLogin = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

                for (int i = 0; i < hashLogin.Length; i++)
                {
                    if (hashLogin[i] != existingUser.PasswordHash[i]) return null;
                }

                var jwt= new JwtDTO
                {
                    Id=existingUser.Id,
                    UserName = existingUser.UserName,
                    email = request.email,
                    drink_counter = existingUser.drink_counter,
                    Token = _tokenService.CreateToken(existingUser)
                };

                return await Task.FromResult(jwt);

            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }

}

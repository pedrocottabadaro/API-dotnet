using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Command.Auth
{
    public record LoginAuthCommand(String Password, String email) : IRequest<Response>;

    public class LoginAuthCommandHandler : IRequestHandler<LoginAuthCommand, Response>
    {

        private readonly IRepositoryUser _repository;
        private readonly ITokenService _tokenService;

        public LoginAuthCommandHandler(IRepositoryUser repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Response> Handle(LoginAuthCommand request, CancellationToken cancellationToken)
        {

            Response r = new Response();
            var existingUser = _repository.GetUserByEmail(request.email).Result;

            using var hmac = new HMACSHA512(existingUser.PasswordSalt);
            var hashLogin = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));

            for (int i = 0; i < hashLogin.Length; i++)
            {
                if (hashLogin[i] != existingUser.PasswordHash[i])
                {
                    r.AddError("Passoword", "Incorret Password");
                    return r;
                }
            }

            var jwt = new JwtDTO
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                email = request.email,
                drink_counter = existingUser.drink_counter,
                Token = _tokenService.CreateToken(existingUser)
            };
            return new Response(jwt);


        }

        public class LoginAuthCommandValidator : AbstractValidator<LoginAuthCommand>
        {
            private readonly IRepositoryUser repository;

            public LoginAuthCommandValidator(IRepositoryUser repository)
            {

                RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetUserByEmail(newUser.email).Result!=null).WithMessage("User not found");

            }
       

        
        }
    }

}

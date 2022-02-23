using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Users.Command
{
    public record CreateUserCommand (String UserName, String Password,String email) : IRequest<string>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, String>
    {

        private readonly IRepositoryUser _repository;


        public CreateUserCommandHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<String> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (_repository.checkUserExists(request.UserName)) return await Task.FromResult("Username is Taken");
                using var hmac = new HMACSHA512();
                var user = new User
                {
                    UserName = request.UserName.ToLower(),
                    email = request.email,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                    PasswordSalt = hmac.Key
                };

                _repository.Add(user);

                return await Task.FromResult("User created");

            }
            catch (System.Exception)
            {
                return await Task.FromResult("Internal Error");
            }

        }
    }
}

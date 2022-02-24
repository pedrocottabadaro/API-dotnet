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
    public record UpdateUserCommand(int Id, String UserName, String Password, String email) : IRequest<string>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, String>
    {

        private readonly IRepositoryUser _repository;


        public UpdateUserCommandHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<String> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _repository.GetById(request.Id);
                if (user == null) return await Task.FromResult("User not found");

                user.email = request.email;

                _repository.Update(user);

                return await Task.FromResult("User updated");

            }
            catch (System.Exception)
            {
                return await Task.FromResult("Internal error");
            }

        }
    }
}

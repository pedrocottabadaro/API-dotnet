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
    public record DeleteUserCommand (int Id) : IRequest<string>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, String>
    {

        private readonly IRepositoryUser _repository;


        public DeleteUserCommandHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<String> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _repository.GetById(request.Id);
                if(user == null) return await Task.FromResult("User not found");
                _repository.Remove(user);

                return await Task.FromResult("User deleted");

            }
            catch (System.Exception)
            {
                return await Task.FromResult("Internal error");
            }

        }
    }
}

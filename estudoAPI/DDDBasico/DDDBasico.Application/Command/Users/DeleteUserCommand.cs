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

namespace DDDBasico.Application.Users.Command
{
    public record DeleteUserCommand(int Id) : IRequest<string>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, String>
    {

        private readonly IRepositoryUser _repository;
        private readonly ITokenService _tokenService;

        public DeleteUserCommandHandler(IRepositoryUser repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }


        public async Task<String> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
               /* if(_tokenService.ReturnIdToken("!") != request.Id.ToString()) return await Task.FromResult("User deleted");*/
                var user = _repository.GetById(request.Id);
                if (user == null) return await Task.FromResult("User not found");
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

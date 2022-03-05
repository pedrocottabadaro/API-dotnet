using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAcessor;


        public DeleteUserCommandHandler(IRepositoryUser repository, ITokenService tokenService, IHttpContextAccessor httpContextAcessor)
        {
            _repository = repository;
            _tokenService = tokenService;
            _httpContextAcessor = httpContextAcessor;
        }


        public async Task<String> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var token = _httpContextAcessor.HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer");
                if (_tokenService.ReturnIdToken(token[1].TrimStart()) != request.Id.ToString()) return "Not authorized";
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

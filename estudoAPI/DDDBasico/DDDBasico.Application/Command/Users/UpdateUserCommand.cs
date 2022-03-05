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
    public record UpdateUserCommand(int Id, String UserName, String Password, String email) : IRequest<string>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, String>
    {

        private readonly IRepositoryUser _repository;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAcessor;


        public UpdateUserCommandHandler(IRepositoryUser repository, ITokenService tokenService, IHttpContextAccessor httpContextAcessor)
        {
            _repository = repository;
            _tokenService = tokenService;
            _httpContextAcessor = httpContextAcessor;
        }

        public async Task<String> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var token = _httpContextAcessor.HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer");
                if (_tokenService.ReturnIdToken(token[1].TrimStart()) != request.Id.ToString()) return "Not authorized";
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

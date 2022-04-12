using DDDBasico.Application.Extras;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using FluentValidation;
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
    public record DeleteUserCommand(int Id) : IRequest<Response>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response>
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


        public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            Response r = new Response();
            var token = _httpContextAcessor.HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer");
            if (_tokenService.ReturnIdToken(token[1].TrimStart()) != request.Id.ToString())
            {
                r.AddError("Token", "Not authorized");
                return r;
            }

            var user = _repository.GetById(request.Id).Result;
          
            _repository.Remove(user);
            return r;

        }

        public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
        {

            private readonly IRepositoryUser repository;

            public DeleteUserCommandValidator(IRepositoryUser repository)
            {

                RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");

            }
        }
    }
}

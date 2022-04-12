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
    public record UpdateUserCommand(int Id, String UserName, String Password, String email) : IRequest<Response>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response>
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

        public async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var token = _httpContextAcessor.HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer");
            if (_tokenService.ReturnIdToken(token[1].TrimStart()) != request.Id.ToString()) return null;
            var user = _repository.GetById(request.Id).Result;
            user.email = request.email;
            _repository.Update(user);
            return new Response();

        }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IRepositoryUser repository;

        public UpdateUserCommandValidator(IRepositoryUser repository)
        {
            RuleFor(updatedUser => updatedUser.UserName).NotEmpty().MaximumLength(100);
            RuleFor(updatedUser => updatedUser.email).NotEmpty().MaximumLength(100).EmailAddress();
            RuleFor(updatedUser => updatedUser.Password).NotEmpty().MaximumLength(12).MinimumLength(4);
            RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");

        }
    }
}

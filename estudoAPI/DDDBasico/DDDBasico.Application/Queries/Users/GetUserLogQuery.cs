using DDDBasico.Application.DTO;
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
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Queries.Users
{
    public record GetUserLogQuery(int Id) : IRequest<Response>;

    public class GetUserLogQueryHandler : IRequestHandler<GetUserLogQuery, Response>
    {

        private readonly IRepositoryLog _repositoryLog;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAcessor;


        public GetUserLogQueryHandler(IRepositoryLog repositoryLog, ITokenService tokenService,IHttpContextAccessor httpContextAcessor)
        {
            _repositoryLog = repositoryLog;
            _tokenService = tokenService;
            _httpContextAcessor = httpContextAcessor;
        }


        public async Task<Response> Handle(GetUserLogQuery request, CancellationToken cancellationToken)
        {
            var token = _httpContextAcessor.HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer");
            if (_tokenService.ReturnIdToken(token[1].TrimStart()) != request.Id.ToString()) return null;
            var userLog = _repositoryLog.GetUserLog(request.Id);
            var result = new LogDTO();
    
            var logs = userLog.Select(log => new LogDTO
            {
                Data = log.Data,
                drink_counter = log.drink_amount
            });
            return new Response(logs);
        }

        public class GetUserLogQueryValidator : AbstractValidator<GetUserLogQuery>
        {

            private readonly IRepositoryUser repository;

            public GetUserLogQueryValidator(IRepositoryUser repository)
            {
                RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");

            }
        }
    }
}

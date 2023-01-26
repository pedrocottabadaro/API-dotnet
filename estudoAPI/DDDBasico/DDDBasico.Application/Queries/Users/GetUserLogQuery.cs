using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DDDBasico.Application.Queries.Users
{
    public record GetUserLogQuery(int Id) : IRequest<Response>;

    public class GetUserLogQueryHandler : IRequestHandler<GetUserLogQuery, Response>
    {

        private readonly IRepositoryLog _repositoryLog;

        public GetUserLogQueryHandler(IRepositoryLog repositoryLog)
        {
            _repositoryLog = repositoryLog;
        }


        public async Task<Response> Handle(GetUserLogQuery request, CancellationToken cancellationToken)
        {
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
            public GetUserLogQueryValidator(IRepositoryUser repository)
            {
                RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");

            }
        }
    }
}

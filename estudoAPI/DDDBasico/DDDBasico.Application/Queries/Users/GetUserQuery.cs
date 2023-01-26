using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
using DDDBasico.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace DDDBasico.Application.Queries.Users
{
    public record GetUserQuery(int Id) : IRequest<Response>;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Response>
    {

        private readonly IRepositoryUser _repository;

        public GetUserQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }


        public async Task<Response> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = _repository.GetById(request.Id).Result;

            return new Response(
            new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                email = user.email,
                drink_counter = user.drink_counter
            });
        }
    }

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator(IRepositoryUser repository)
        {
            RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");

        }
    }
}

using DDDBasico.Application.Extras;
using DDDBasico.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace DDDBasico.Application.Users.Command
{
    public record DeleteUserCommand(int Id) : IRequest<Response>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response>
    {

        private readonly IRepositoryUser _repository;

        public DeleteUserCommandHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }


        public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _repository.GetById(request.Id).Result;
            _repository.Remove(user);

            return new Response(request.Id);
        }

        public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
        {

            public DeleteUserCommandValidator(IRepositoryUser repository)
            {

                RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");

            }
        }
    }
}

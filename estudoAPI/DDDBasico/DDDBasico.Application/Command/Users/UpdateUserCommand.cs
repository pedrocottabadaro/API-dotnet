using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
using DDDBasico.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace DDDBasico.Application.Users.Command
{
    public record UpdateUserCommand(int Id, String UserName, String Password, String email) : IRequest<Response>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response>
    {

        private readonly IRepositoryUser _repository;

        public UpdateUserCommandHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _repository.GetById(request.Id).Result;
            user.email = request.email;
            _repository.Update(user);

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

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IRepositoryUser repository)
        {
            RuleFor(updatedUser => updatedUser.UserName).NotEmpty().MaximumLength(100);
            RuleFor(updatedUser => updatedUser.email).NotEmpty().MaximumLength(100).EmailAddress();
            RuleFor(updatedUser => updatedUser.Password).NotEmpty().MaximumLength(12).MinimumLength(4);
            RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");

        }
    }
}

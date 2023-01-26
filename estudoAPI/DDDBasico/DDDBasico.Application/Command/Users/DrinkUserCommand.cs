using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace DDDBasico.Application.Users.Command
{
    public record DrinkUserCommand(int Id, int drink_ml) : IRequest<Response>;

    public class DrinkUserCommandHandler : IRequestHandler<DrinkUserCommand, Response>
    {

        private readonly IRepositoryUser _repository;
        private readonly IRepositoryLog _log;


        public DrinkUserCommandHandler(IRepositoryUser repository, IRepositoryLog log)
        {
            _repository = repository;
            _log = log;
    }

        public async Task<Response> Handle(DrinkUserCommand request, CancellationToken cancellationToken)
        {
        
            var user = _repository.GetById(request.Id).Result;
            user.drink_counter += 0 + request.drink_ml;
            var currentDateTime=DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            var dateLog = DateTime.Parse(currentDateTime);
            var userLog = new Log {
                Data = dateLog,
                drink_amount = request.drink_ml,
                Iduser = user.Id
            };
            _repository.Update(user);
            _log.Add(userLog);
        
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


    public class DrinkUserCommandValidator : AbstractValidator<DrinkUserCommand>
    {
        public DrinkUserCommandValidator(IRepositoryUser repository)
        {
            RuleFor(drinkUser => drinkUser.drink_ml).NotEmpty();
            RuleFor(newUser => newUser).MustAsync(async (newUser, _) => repository.GetById(newUser.Id).Result == null).WithMessage("User not found");


        }
    }
}

using DDDBasico.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Users.Command
{
    public record CreateUserCommand (String Username,byte[]Password,String email) : IRequest<string>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {

        private readonly IRepositoryUser _repository;
        private readonly IMediator _mediator;


        public CreateUserCommandHandler(IMediator mediator,IRepositoryUser repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
      /*      var user = _repository.GetById(request.Id);
            Console.WriteLine(user);*/
            return await Task.FromResult("Sucesso");
        }
    }
}

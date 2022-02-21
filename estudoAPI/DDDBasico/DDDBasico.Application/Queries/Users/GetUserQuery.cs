using DDDBasico.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Queries.Users
{
    public record GetUserQuery(String Username, byte[] Password, String email) : IRequest<string>;

    public class CreateUserQueryHandler : IRequestHandler<GetUserQuery, string>
    {

        private readonly IRepositoryUser _repository;

        public CreateUserQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            /*var users = await _repository.GetAll();

            return Task.FromResult("Sucesso");*/
            return null;
           
        }
    }
}

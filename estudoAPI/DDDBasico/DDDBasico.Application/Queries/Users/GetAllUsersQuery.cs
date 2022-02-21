using DDDBasico.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Queries.Users
{
    public record GetAllUsersQuery() : IRequest<string>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, string>
    {

        private readonly IRepositoryUser _repository;

        public GetAllUsersQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public Task<string> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _repository.GetAll();
            Console.WriteLine(users);
            return Task.FromResult("Sucesso");


        }
    }
}

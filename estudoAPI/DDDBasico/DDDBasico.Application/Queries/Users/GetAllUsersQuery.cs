using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Queries.Users
{
    public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {

        private readonly IRepositoryUser _repository;

        public GetAllUsersQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task <IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _repository.GetAll();
            return await Task.FromResult(users);

        }

    }
}

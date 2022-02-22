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
    public record GetUserQuery(int Id) : IRequest<User>;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {

        private readonly IRepositoryUser _repository;

        public GetUserQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = _repository.GetById(1);
            return await Task.FromResult(result);
        }
    }
}

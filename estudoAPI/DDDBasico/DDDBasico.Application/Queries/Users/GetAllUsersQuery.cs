using DDDBasico.Application.DTO;
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
    public record GetAllUsersQuery() : IRequest<IEnumerable<UserDTO>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {

        private readonly IRepositoryUser _repository;

        public GetAllUsersQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task <IEnumerable<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _repository.GetAll().Select(user=> new UserDTO {
                Id = user.Id,
                UserName = user.UserName,
                email = user.email,
                drink_counter = user.drink_counter
            });
            return await Task.FromResult(users);

        }

    }
}

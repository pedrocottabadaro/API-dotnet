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
    public record GetUserQuery(int Id) : IRequest<UserDTO>;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDTO>
    {

        private readonly IRepositoryUser _repository;

        public GetUserQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = _repository.GetById(request.Id);

            var result = new UserDTO {
                Id = user.Id,
                UserName= user.UserName,
                email= user.email,
                drink_counter= user.drink_counter
            };
            return await Task.FromResult(result);
        }
    }
}

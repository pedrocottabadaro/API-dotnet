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
    public record GetRankingUsersQuery() : IRequest<UserDTO>;

    public class GetRankingUsersQueryHandler : IRequestHandler<GetRankingUsersQuery, UserDTO>
    {

        private readonly IRepositoryLog _repositoryLog;

        public GetRankingUsersQueryHandler(IRepositoryLog repositoryLog)
        {
            _repositoryLog = repositoryLog;
        }

        public async Task <UserDTO> Handle(GetRankingUsersQuery request, CancellationToken cancellationToken)
        {
            var users = repositoryLog.GetRanking().Select(user=> new UserDTO {
                Id = user.Id,
                UserName = user.UserName,
                email = user.email,
                drink_counter = user.drink_counter
            });
            return await Task.FromResult(users);

        }

    }
}

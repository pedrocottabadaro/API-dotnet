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
    public record GetRankingUsersQuery() : IRequest<Object>;

    public class GetRankingUsersQueryHandler : IRequestHandler<GetRankingUsersQuery, Object>
    {

        private readonly IRepositoryLog _repositoryLog;

        public GetRankingUsersQueryHandler(IRepositoryLog repositoryLog)
        {
            _repositoryLog = repositoryLog;
        }

        public async Task <Object> Handle(GetRankingUsersQuery request, CancellationToken cancellationToken)
        {
            var topRankedUser = _repositoryLog.GetRanking();
            return await Task.FromResult(topRankedUser);

        }

    }
}

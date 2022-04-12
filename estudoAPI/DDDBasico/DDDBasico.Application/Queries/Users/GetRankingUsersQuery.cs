using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
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
    public record GetRankingUsersQuery() : IRequest<Response>;

    public class GetRankingUsersQueryHandler : IRequestHandler<GetRankingUsersQuery, Response>
    {

        private readonly IRepositoryLog _repositoryLog;

        public GetRankingUsersQueryHandler(IRepositoryLog repositoryLog)
        {
            _repositoryLog = repositoryLog;
        }

        public async Task <Response> Handle(GetRankingUsersQuery request, CancellationToken cancellationToken)
        {
            var topRankedUser = _repositoryLog.GetRanking();
            return new Response(topRankedUser);

        }

    }
}

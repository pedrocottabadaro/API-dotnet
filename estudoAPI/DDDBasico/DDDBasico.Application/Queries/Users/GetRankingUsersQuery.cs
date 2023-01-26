using DDDBasico.Application.Extras;
using DDDBasico.Domain.Interfaces;
using MediatR;

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

using DDDBasico.Application.DTO;
using DDDBasico.Application.Extras;
using DDDBasico.Domain.Interfaces;
using MediatR;

namespace DDDBasico.Application.Queries.Users
{
    public record GetAllUsersQuery() : IRequest<Response>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response>
    {

        private readonly IRepositoryUser _repository;

        public GetAllUsersQueryHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task <Response> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _repository.GetAll().Result.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                email = user.email,
                drink_counter = user.drink_counter
            }); ;
            
            return new Response(users);

        }

    }
}

using DDDBasico.Application.DTO;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
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
        private readonly ITokenService _tokenService;

        public GetUserQueryHandler(IRepositoryUser repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }


        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            /*if (_tokenService.ReturnIdToken("eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwibmJmIjoxNjQ1ODEzODQxLCJleHAiOjE2NDY0MTg2NDEsImlhdCI6MTY0NTgxMzg0MX0.Hdxg7SP3HOQQba4xHO8nyrEhFfZPJ7CWrkdvaI8rMk834xd1wPIIIIxunLzkalAmwDlgzeli5WiSbrMM40v5-g") != request.Id.ToString()) return null;*/
            var user = _repository.GetById(request.Id);
            var result = new UserDTO();
            if (user == null) return await Task.FromResult(result);

            result = new UserDTO {
                Id = user.Id,
                UserName= user.UserName,
                email= user.email,
                drink_counter= user.drink_counter
            };
            return await Task.FromResult(result);
        }
    }
}

using DDDBasico.Application.DTO;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAcessor;

        public GetUserQueryHandler(IRepositoryUser repository, ITokenService tokenService, IHttpContextAccessor httpContextAcessor)
        {
            _repository = repository;
            _tokenService = tokenService;
            _httpContextAcessor = httpContextAcessor;
        }


        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var token = _httpContextAcessor.HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer");
            if (_tokenService.ReturnIdToken(token[1].TrimStart()) != request.Id.ToString()) return null;
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

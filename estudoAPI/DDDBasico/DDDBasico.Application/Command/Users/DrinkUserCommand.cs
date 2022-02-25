using DDDBasico.Application.DTO;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Domain.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Users.Command
{
    public record DrinkUserCommand(int Id, int drink_ml) : IRequest<UserDTO>;

    public class DrinkUserCommandHandler : IRequestHandler<DrinkUserCommand, UserDTO>
    {

        private readonly IRepositoryUser _repository;
        private readonly IRepositoryLog _log;
        private readonly ITokenService _tokenService;


        public DrinkUserCommandHandler(IRepositoryUser repository, IRepositoryLog log, ITokenService tokenService)
        {
            _repository = repository;
            _log = log;
            _tokenService = tokenService;
        }

        public async Task<UserDTO> Handle(DrinkUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
               /* if (_tokenService.ReturnIdToken("!") != request.Id.ToString()) return null;*/
                var user = _repository.GetById(request.Id);
                if (user == null) return null;

                user.drink_counter += 0 + request.drink_ml;
                var currentDateTime=DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                var dateLog = DateTime.Parse(currentDateTime);
                var userLog = new Log {
                    Data = dateLog,
                    drink_amount = request.drink_ml,
                    Iduser = user.Id
                };
                _repository.Update(user);
                _log.Add(userLog);


                var result = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    email = user.email,
                    drink_counter = user.drink_counter
                };
                return await Task.FromResult(result);


            }
            catch (System.Exception)
            {
                /*  return await Task.FromResult("Erro");*/
                return null;
            }

        }
    }
}

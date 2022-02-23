using DDDBasico.Application.DTO;
using DDDBasico.Domain.Entities;
using DDDBasico.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DDDBasico.Application.Users.Command
{
    public record DrinkUserCommand (int Id,int drink_ml) : IRequest<UserDTO>;

    public class DrinkUserCommandHandler : IRequestHandler<DrinkUserCommand, UserDTO>
    {

        private readonly IRepositoryUser _repository;


        public DrinkUserCommandHandler(IRepositoryUser repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO> Handle(DrinkUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _repository.GetById(request.Id);
                if (user == null) return null;

                user.drink_counter +=0+request.drink_ml;

                _repository.Update(user);

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

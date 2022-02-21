using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDBasico.Application.Queries.Users;
using DDDBasico.Application.Users.Command;
using DDDBasico.Domain.Interfaces;
using DDDBasico.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDBasico.API.Controllers
{
    [ApiController]
    [Route("api/users/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IRepositoryUser _repository;
        private readonly IMediator _mediator;


        public UserController(IRepositoryUser repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllUsersQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetUserQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }




    }
}
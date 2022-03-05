using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDBasico.Application.Queries.Users;
using DDDBasico.Application.Users.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDBasico.API.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsersQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [Route("ranking")]
        [HttpGet]
        public async Task<IActionResult> Ranking()
        {
            var query = new GetRankingUsersQuery();
            var response = await _mediator.Send(query);
            if (response != null) return Ok(response);
            return BadRequest("Something went wrong");

        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetUserQuery(id);
            var request = query with { Id = id };
            var response = await _mediator.Send(request);
            if (response !=null) return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("{id}/log")]
        [Authorize]
        public async Task<IActionResult> GetDrinkingLog(int id)
        {
            var query = new GetUserLogQuery(id);
            var request = query with { Id = id };
            var response = await _mediator.Send(request);
            if (response != null) return Ok(response);
            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response == "User created") return Ok(response);
            if (response == "User already exists") return BadRequest(response);
            return StatusCode(500, response);

        }
 
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] UpdateUserCommand command)
        {
            var request = command with { Id = id};
            var response = await _mediator.Send(request);
            if (response == "User updated") return Ok(response);
            if (response == "User not found") return NotFound(response);
            return StatusCode(500,response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id,DeleteUserCommand command)
        {
            var request = command with { Id = id };
            var response = await _mediator.Send(request);
            if (response == "User deleted") return Ok(response);
            if (response == "User not found") return NotFound(response);
            return StatusCode(500, response);
        }

     
        [HttpPost("{id}/drink")]
        [Authorize]
        public async Task<IActionResult> Drink(int id,[FromBody]DrinkUserCommand command)
        {
            var request = command with { Id = id };
            var response = await _mediator.Send(request);
            if(response!=null) return Ok(response);
            return BadRequest("Something went wrong");

        }




    }
}
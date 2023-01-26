using DDDBasico.Application.Middleware;
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
            if (response.Errors.Count()!= 0) return UnprocessableEntity();
            return Ok(response.Data);
        }


        [Route("ranking")]
        [HttpGet]
        public async Task<IActionResult> Ranking()
        {
            var query = new GetRankingUsersQuery();
            var response = await _mediator.Send(query);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);

        }

        [HttpGet("{id}")]
        [Authorize]
        [AuthorizeByUserId]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var query = new GetUserQuery(id);
            var request = query with { Id = id };
            var response = await _mediator.Send(request);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);
        }

        [HttpGet("{id}/log")]
        [Authorize]
        [AuthorizeByUserId]

        public async Task<IActionResult> GetDrinkingLog([FromRoute] int id)
        {
            var query = new GetUserLogQuery(id);
            var request = query with { Id = id };
            var response = await _mediator.Send(request);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);

        }

        [HttpPut("{id}")]
        [Authorize]
        [AuthorizeByUserId]

        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateUserCommand command)
        {
            var request = command with { Id = id };
            var response = await _mediator.Send(request);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [AuthorizeByUserId]

        public async Task<IActionResult> Delete([FromRoute] int id,DeleteUserCommand command)
        {
            var request = command with { Id = id };
            var response = await _mediator.Send(request);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);
        }

     
        [HttpPost("{id}/drink")]
        [Authorize]
        [AuthorizeByUserId]

        public async Task<IActionResult> Drink([FromRoute] int id,[FromBody]DrinkUserCommand command)
        {
            var request = command with { Id = id };
            var response = await _mediator.Send(request);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);

        }

    }
}
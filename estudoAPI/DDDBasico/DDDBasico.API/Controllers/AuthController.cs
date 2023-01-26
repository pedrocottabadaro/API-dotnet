using DDDBasico.Application.Command.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDBasico.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginAuthCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Errors.Count() != 0) return UnprocessableEntity();
            return Ok(response.Data);

        }
    }
}

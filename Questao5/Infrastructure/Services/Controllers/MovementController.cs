using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/movement/")]
    public class MovementController : Controller
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromServices]IMediator mediator, [FromBody]CreateMovementRequest command)
        {
            var response = await mediator.Send(command);

            if (response.Result == 400)
            {
               return BadRequest(response);
            }

            return Ok(response);
        }
    }
}

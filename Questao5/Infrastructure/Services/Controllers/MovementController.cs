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
        public Task<CreateMovementResponse> Create([FromServices]IMediator mediator, [FromBody]CreateMovementRequest command)
        {
            var response = mediator.Send(command);
            return response;
        }
    }
}

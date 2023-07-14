using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/accountbalance/")]
    public class AccountBalanceController : Controller
    {
        [HttpGet]
        [Route("{accountNumber}")]
        public async Task<IActionResult> Create([FromServices] IMediator mediator, [FromRoute] string accountNumber)
        {
            var response = await mediator.Send(new GetAccountBalanceRequest() { AccountNumber = accountNumber});

            if (response.Result == 400)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}

using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class GetAccountBalanceRequest : IRequest<GetAccountBalanceResponse>
    {
        public string AccountNumber { get; set; }
    }
}

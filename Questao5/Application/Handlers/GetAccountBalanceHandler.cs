using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Application.Handlers
{
    public class GetAccountBalanceHandler : IRequestHandler<GetAccountBalanceRequest, GetAccountBalanceResponse>
    {
        private readonly IMediator _mediator;
        private readonly IAccountService _accountValidator;

        public GetAccountBalanceHandler(IMediator mediator, IAccountService accountValidator)
        {
            _mediator = mediator;
            _accountValidator = accountValidator;
        }

        public async Task<GetAccountBalanceResponse> Handle(GetAccountBalanceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var accountData = await GetAccountData(request.AccountNumber);

                _accountValidator.ValidateAccount(accountData);

                var accountBalance = await GetAccountBalance(accountData.IdContaCorrente);

                var response = CreateResponse("", 200, new GetAccountBalanceResponse { AccountNumber = accountBalance.Numero, AccountHolder = accountBalance.Nome, BalanceValue = accountBalance.Saldo, ConsultationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") });

                return response;

            }
            catch (Exception e)
            {
                var response = CreateResponse(e.Message, 400);

                return await Task.FromResult(response);
            }

        }
        private GetAccountBalanceResponse CreateResponse(string description, int result, GetAccountBalanceResponse? response = null)
        {
            if (response == null)
                return new GetAccountBalanceResponse { Description = description, Result = result };

            return new GetAccountBalanceResponse
            {
                AccountNumber = response.AccountNumber,
                AccountHolder = response.AccountHolder,
                ConsultationDate = response.ConsultationDate,
                BalanceValue = response.BalanceValue
            };
        }

        private async Task<GetAccountBalanceQueryResponse> GetAccountBalance(string accountNumber)
        {
            return await _mediator.Send(new GetAccountBalanceQueryRequest { Numero = accountNumber });
        }
        private async Task<GetIdAccountFindByNumberQueryResponse> GetAccountData(string accountNumber)
        {
            return await _mediator.Send(new GetIdAccountFindByNumberQueryRequest { Numero = accountNumber });
        }
    }
}

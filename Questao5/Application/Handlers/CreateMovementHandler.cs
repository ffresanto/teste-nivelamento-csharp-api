using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Constants;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Application.Handlers
{
    public class CreateMovementHandler : IRequestHandler<CreateMovementRequest, CreateMovementResponse>
    {
        private readonly IMediator _mediator;
        private readonly IAccountService _accountService;
        public CreateMovementHandler(IMediator mediator, IAccountService accountService)
        {
            _mediator = mediator;
            _accountService = accountService;
        }
        public async Task<CreateMovementResponse> Handle(CreateMovementRequest request, CancellationToken cancellationToken)
        {
            try
            {  
                var idempotenciaData = await GetIdempotenciaData(request.IdRequest);

                if (idempotenciaData == null)
                    await CreateIdempotencia(request.IdRequest, IdempotenciaResultType.IN_PROGRESS.ToString(), nameof(CreateMovementRequest));

                if (idempotenciaData != null && idempotenciaData.Resultado == IdempotenciaResultType.CONCLUDED.ToString())
                    return CreateResponse(idempotenciaData.Resultado, ConstantsResponse.RequestAlreadyMade, 400);

                if (idempotenciaData != null && idempotenciaData.Resultado == IdempotenciaResultType.ERROR.ToString())
                    return CreateResponse(idempotenciaData.Resultado, ConstantsResponse.RequestAlreadyMade, 400);

                var accountData = await GetAccountData(request.AccountNumber);

                _accountService.ValidateAccount(accountData);
                _accountService.ValidateValue(request.Value);
                ValidateTypeMovement(request.TypeMovement);

                var commandRequest = new CreateMovementCommandRequest
                {
                    IdContaCorrente = accountData.IdContaCorrente,
                    TipoMovimento = request.TypeMovement.ToUpper(),
                    Valor = request.Value
                };

                var responseCommand = await CreateMovement(commandRequest);

                await UpdateIdempotencia(request.IdRequest, IdempotenciaResultType.CONCLUDED.ToString());

                var response = CreateResponse(responseCommand.Description, commandRequest.IdMovimento, 200);

                return await Task.FromResult(response);
            }
            catch (Exception e)
            {
                await UpdateIdempotencia(request.IdRequest, IdempotenciaResultType.ERROR.ToString());

                var response = CreateResponse(e.Message, null, 400);

                return await Task.FromResult(response);
            }
        }

        private async Task<CreateIdempotenciaCommandResponse> CreateIdempotencia(string idRequest, string result, string Requisicao)
        {
            return await _mediator.Send(new CreateIdempotenciaCommandRequest { IdRequisicao = idRequest, Resultado = result, Requisicao = Requisicao });
        }

        private async Task<GetIdempotenciaQueryResponse> GetIdempotenciaData(string idRequest)
        {
            return await _mediator.Send(new GetIdempotenciaQueryRequest { IdRequisicao = idRequest });
        }

        private async Task<GetIdAccountFindByNumberQueryResponse> GetAccountData(string accountNumber)
        {
            return await _mediator.Send(new GetIdAccountFindByNumberQueryRequest { Numero = accountNumber });
        }

        private void ValidateTypeMovement(string typeMovement)
        {
            if (typeMovement.ToUpper() != "C" && typeMovement.ToUpper() != "D")
                throw new Exception(ErrorType.INVALID_TYPE.ToString());
        }

        private async Task UpdateIdempotencia(string idRequest, string result)
        {
            await _mediator.Send(new UpdateIdempotenciaCommandRequest { IdRequisicao = idRequest, Resultado = result });
        }

        private async Task<CreateMovementCommandResponse> CreateMovement(CreateMovementCommandRequest commandRequest)
        {
            return await _mediator.Send(commandRequest);
        }

        private CreateMovementResponse CreateResponse(string description, string idMovimento, int result)
        {
            return new CreateMovementResponse
            {
                IdMovimento = idMovimento,
                Description = description,
                Result = result
            };
        }
    }
}

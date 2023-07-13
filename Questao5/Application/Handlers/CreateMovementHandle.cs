using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Constants;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Requests;

namespace Questao5.Application.Handlers
{
    public class CreateMovementHandle : IRequestHandler<CreateMovementRequest, CreateMovementResponse>
    {
        private readonly IMediator _mediator;
        public CreateMovementHandle(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreateMovementResponse> Handle(CreateMovementRequest request, CancellationToken cancellationToken)
        {
            var accountData = await _mediator.Send(new GetIdAccountFindByNumberQueryRequest { Numero = request.NumeroConta });

            // Valida o idempotencia
            var IdempotenciaData = await _mediator.Send(new GetIdempotenciaQueryRequest { IdRequisicao = request.IdRequisicao });
            
            if (IdempotenciaData != null && IdempotenciaData.Resultado == ConstantsIdempotencia.Error) 
                return await Task.FromResult(new CreateMovementResponse{ IdMovimento = "0", Description = IdempotenciaData.Resultado });

            // Inseri Registro de idempotencia em andamento
            if (IdempotenciaData != null && IdempotenciaData.Resultado != ConstantsIdempotencia.InProgress)
                await _mediator.Send(new CreateIdempotenciaCommandRequest { IdRequisicao = request.IdRequisicao, Requisicao = "Movement", Resultado = ConstantsIdempotencia.InProgress });

            var commandRequest = new CreateMovementCommandRequest
            {
                IdContaCorrente = request.NumeroConta,
                TipoMovimento = request.TipoMovimento,
                Valor = request.Valor
            };

            var responseCommand = await _mediator.Send(commandRequest);

            // Atualiza Registro de idempotencia Concluido
            await _mediator.Send(new UpdateIdempotenciaCommandRequest { IdRequisicao = request.IdRequisicao, Resultado = ConstantsIdempotencia.Concluded });

            var response = new CreateMovementResponse
            {
                IdMovimento = commandRequest.IdMovimento,
                Description = responseCommand.Description
            };

            return await Task.FromResult(response);
        }
    }
}

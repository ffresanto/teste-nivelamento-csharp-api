using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.CommandStore.Requests;

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
            // Valida o idempotencia

            // se existir retorna o idempotencia resultado.

            // Se nao existir valida request.

            var commandRequest = new CreateMovementCommandRequest
            {
                IdContaCorrente = request.NumeroConta,
                TipoMovimento = request.TipoMovimento,
                Valor = request.Valor
            };

            var responseCommand = await _mediator.Send(commandRequest);

            var response = new CreateMovementResponse
            {
                IdMovimento = commandRequest.IdMovimento,
                Description = responseCommand.Description
            };

            return await Task.FromResult(response);
        }
    }
}

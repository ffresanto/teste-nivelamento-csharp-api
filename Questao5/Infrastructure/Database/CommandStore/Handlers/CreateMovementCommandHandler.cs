using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Constants;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Handlers
{
    public class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommandRequest, CreateMovementCommandResponse>
    {
        private readonly SqliteConnection _connection;

        public CreateMovementCommandHandler(SqliteConnection connection)
        {
            _connection = connection;
        }

        async Task<CreateMovementCommandResponse> IRequestHandler<CreateMovementCommandRequest, CreateMovementCommandResponse>.Handle(CreateMovementCommandRequest request, CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO movimento (idcontacorrente, idmovimento, datamovimento, tipomovimento, valor) " +
            "VALUES (@IdContaCorrente, @IdMovimento, @DataMovimento, @TipoMovimento, @Valor)";

            await _connection.ExecuteAsync(sql, request);

            return new() { Description = ConstantsResponse.InsertedSuccessfully };
        }
    }
}

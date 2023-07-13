using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Questao5.Infrastructure.Database.CommandStore.Handlers
{
    public class UpdateIdempotenciaCommandHandler : IRequestHandler<UpdateIdempotenciaCommandRequest, UpdateIdempotenciaCommandResponse>
    {
        private readonly SqliteConnection _connection;

        public UpdateIdempotenciaCommandHandler(SqliteConnection connection)
        {
            _connection = connection;
        }
        async public Task<UpdateIdempotenciaCommandResponse> Handle(UpdateIdempotenciaCommandRequest request, CancellationToken cancellationToken)
        {
            var sql = "UPDATE idempotencia SET resultado = @Resultado WHERE chave_idempotencia = @IdRequisicao";

            await _connection.ExecuteAsync(sql, request);

            return new UpdateIdempotenciaCommandResponse { IdRequisicao = request.IdRequisicao, Resultado = request.Resultado };
        }

    }
}

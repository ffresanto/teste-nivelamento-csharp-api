using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Handlers
{
    public class CreateIdempotenciaCommandHandler : IRequestHandler<CreateIdempotenciaCommandRequest, CreateIdempotenciaCommandResponse>
    {
        private readonly SqliteConnection _connection;

        public CreateIdempotenciaCommandHandler(SqliteConnection connection)
        {
            _connection = connection;
        }

        async public Task<CreateIdempotenciaCommandResponse> Handle(CreateIdempotenciaCommandRequest request, CancellationToken cancellationToken)
        {
            var sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado ) VALUES (@IdRequisicao, @Requisicao, @Resultado)";

            await _connection.ExecuteAsync(sql, request);

            return new() { IdRequisicao = request.IdRequisicao, Requisicao = request.Requisicao, Resultado = request.Resultado };
        }
    }
}

using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers
{
    public class GetIdempotenciaQueryHandler : IRequestHandler<GetIdempotenciaQueryRequest, GetIdempotenciaQueryResponse>
    {
        private readonly SqliteConnection _connection;
        public GetIdempotenciaQueryHandler(SqliteConnection connection)
        {
            _connection = connection;
        }

        async public Task<GetIdempotenciaQueryResponse> Handle(GetIdempotenciaQueryRequest request, CancellationToken cancellationToken)
        {
            var sql = "SELECT chave_idempotencia, requisicao, resultado FROM idempotencia WHERE chave_idempotencia = @IdRequisicao";

            var result = await _connection.QueryFirstOrDefaultAsync<GetIdempotenciaQueryResponse>(sql, request);

            return result;
        }
    }
}

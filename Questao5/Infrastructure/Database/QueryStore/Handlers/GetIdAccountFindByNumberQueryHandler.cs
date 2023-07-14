using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers
{
    public class GetIdAccountFindByNumberQueryHandler : IRequestHandler<GetIdAccountFindByNumberQueryRequest, GetIdAccountFindByNumberQueryResponse>
    {
        private readonly SqliteConnection _connection;
        public GetIdAccountFindByNumberQueryHandler(SqliteConnection connection)
        {
            _connection = connection;
        }

        public async Task<GetIdAccountFindByNumberQueryResponse> Handle(GetIdAccountFindByNumberQueryRequest request, CancellationToken cancellationToken)
        {
            var sql = "SELECT idcontacorrente, numero,  nome, ativo FROM contacorrente WHERE numero = @Numero OR idcontacorrente = @Numero";

            var result = await _connection.QueryFirstOrDefaultAsync<GetIdAccountFindByNumberQueryResponse>(sql, request);

            return result;
        }

    }
}

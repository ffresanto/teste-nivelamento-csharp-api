using Dapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Handlers
{
    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQueryRequest, GetAccountBalanceQueryResponse>
    {
        private readonly SqliteConnection _connection;
        public GetAccountBalanceQueryHandler(SqliteConnection connection)
        {
            _connection = connection;
        }
        public async Task<GetAccountBalanceQueryResponse> Handle(GetAccountBalanceQueryRequest request, CancellationToken cancellationToken)
        {
            var sql = @"SELECT cc.idcontacorrente, cc.numero, cc.nome,
                        COALESCE(SUM(CASE WHEN m.tipomovimento = 'C' THEN m.valor ELSE 0 END), 0) - COALESCE(SUM(CASE WHEN m.tipomovimento = 'D' THEN m.valor ELSE 0 END), 0) AS saldo
                        FROM contacorrente cc
                        LEFT JOIN movimento m ON cc.idcontacorrente = m.idcontacorrente
                        WHERE cc.idcontacorrente = @Numero
                        GROUP BY cc.idcontacorrente, cc.numero, cc.nome;";

            var result = await _connection.QueryFirstOrDefaultAsync<GetAccountBalanceQueryResponse>(sql, request);

            return result;
        }
    }
}

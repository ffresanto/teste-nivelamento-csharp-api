using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class GetIdempotenciaQueryRequest : IRequest<GetIdempotenciaQueryResponse>
    {
        public string IdRequisicao { get; set; }
    }
}

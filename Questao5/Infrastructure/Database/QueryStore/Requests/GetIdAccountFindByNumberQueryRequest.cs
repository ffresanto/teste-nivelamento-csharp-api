using MediatR;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class GetIdAccountFindByNumberQueryRequest : IRequest<GetIdAccountFindByNumberQueryResponse>
    {
        public string Numero { get; set; }
    }
}

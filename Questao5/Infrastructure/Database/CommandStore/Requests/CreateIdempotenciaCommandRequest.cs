using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class CreateIdempotenciaCommandRequest : IRequest<CreateIdempotenciaCommandResponse>
    {
        public string IdRequisicao { get; set; }
        public string Requisicao { get; set; }
        public string Resultado { get; set; }
    }
}

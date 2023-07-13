using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class UpdateIdempotenciaCommandRequest : IRequest<UpdateIdempotenciaCommandResponse>
    {
        public string IdRequisicao { get; set; }
        public string Resultado { get; set; }
    }
}

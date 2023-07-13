using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Database.CommandStore.Responses
{
    public class UpdateIdempotenciaCommandResponse
    {
        public string IdRequisicao { get; set; }
        public string Resultado { get; set; }
    }
}

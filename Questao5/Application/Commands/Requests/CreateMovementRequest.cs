using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovementRequest : IRequest<CreateMovementResponse>
    {
        public string IdRequisicao { get; set; }
        public string NumeroConta { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; }
    }
}

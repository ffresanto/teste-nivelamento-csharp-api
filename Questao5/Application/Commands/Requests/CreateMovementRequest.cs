using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovementRequest : IRequest<CreateMovementResponse>
    {
        public string NumeroConta { get; set; }
        public decimal Valor { get; set; }
        // public MovementType TipoMovimento { get; set; }
        public string Description { get; set; }
    }
}

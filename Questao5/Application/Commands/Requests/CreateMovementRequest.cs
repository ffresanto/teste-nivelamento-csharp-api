using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovementRequest
    {
        public string NumeroConta { get; set; }
        public decimal Valor { get; set; }
        public MovementType TipoMovimento { get; set; }
        public string Description { get; set; }
    }
}

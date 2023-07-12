using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class CreateMovementResponse
    {
        public string IdMovimento { get; set; }
        public MovementErrorType TypeError { get; set; }
        public string Description { get; set; }
    }
}

using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CreateMovementRequest : IRequest<CreateMovementResponse>
    {
        public string IdRequest { get; set; }
        public string AccountNumber { get; set; }
        public decimal Value { get; set; }
        public string TypeMovement { get; set; }
    }
}

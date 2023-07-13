using MediatR;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class CreateMovementCommandRequest : IRequest<CreateMovementCommandResponse>
    {
        public string IdMovimento{ get; set; }
        public string IdContaCorrente { get; set; }
        public DateTime DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public Decimal Valor { get; set; }
        public CreateMovementCommandRequest()
        {
            Guid uuid = Guid.NewGuid();
            IdMovimento = uuid.ToString().ToUpper();
            DataMovimento = DateTime.Now;
        }
    }
}

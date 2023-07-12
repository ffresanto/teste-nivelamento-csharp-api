using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Handlers
{
    public class CreateMovementHandle : IRequestHandler<CreateMovementRequest, CreateMovementResponse>
    {
        public CreateMovementHandle()
        {

        }
        public Task<CreateMovementResponse> Handle(CreateMovementRequest request, CancellationToken cancellationToken)
        {
            var result = new CreateMovementResponse { IdMovimento = "1", Description = "Teste descripton" };

            return Task.FromResult(result);
        }
    }
}

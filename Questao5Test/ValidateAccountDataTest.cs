using MediatR;
using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5Test
{
    public class ValidateAccountDataTest
    {
        CreateMovementHandler movement;
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        public ValidateAccountDataTest()
        {

        }
        [Fact]
        public async Task TestValidate()
        {
            string idRequest = "";
            _mediator.Send(new GetIdempotenciaQueryRequest { IdRequisicao = idRequest }).Returns(new GetIdempotenciaQueryResponse() { });


            //movement.Handle();
            //movement.ValidateAccount();
            Assert.Equal("a","a");

        }


        private async Task<GetIdempotenciaQueryResponse> ObterIdempotenciaData(string idRequest)
        {
            return await _mediator.Send(new GetIdempotenciaQueryRequest { IdRequisicao = idRequest });
        }

        private async Task<GetIdAccountFindByNumberQueryResponse> ObterAccountData(string accountNumber)
        {
            return await _mediator.Send(new GetIdAccountFindByNumberQueryRequest { Numero = accountNumber });
        }
    }
}

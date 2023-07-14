using MediatR;
using NSubstitute;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Application.Services;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5Test
{
    public class ValidateMovementTest
    {
        private readonly CreateMovementHandler _createMovementHandler;
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly IAccountService _service = new AccountService();
        public ValidateMovementTest()
        {
            _createMovementHandler = new CreateMovementHandler(_mediator,_service);
        }

        [Fact]
        public async Task TestValidateMovementType()
        {
            const string idRequestTest = "4f20ca9c-21e5-11ee-be56-0242ac120002";
            const string IdContaCorrentTest = "41d3539a-21e6-11ee-be56-0242ac120002";
            const string Ativo = "1";
            const string Nome = "Franccesco Felipe";

            _mediator.Send(Arg.Any<GetIdempotenciaQueryRequest>()).Returns(new GetIdempotenciaQueryResponse() { 
                Chave_Idempotencia = "", 
                Requisicao = "", 
                Resultado= ""
            });

            _mediator.Send(Arg.Any<GetIdAccountFindByNumberQueryRequest>()).Returns(new GetIdAccountFindByNumberQueryResponse() {
                IdContaCorrente = IdContaCorrentTest, 
                Ativo = Ativo, 
                Nome = Nome, 
                Numero = 121
            });

            var result = await _createMovementHandler.Handle(new () {NumeroConta = "001", IdRequisicao = idRequestTest, TipoMovimento = "Ab", Valor = 100 }, new CancellationToken() { });
            var expectResult = new CreateMovementResponse() { Description = ErrorType.INVALID_TYPE.ToString(), IdMovimento = null, Result = 400 };
            Assert.Equal(expectResult.Description, result.Description);

        }
    }
}

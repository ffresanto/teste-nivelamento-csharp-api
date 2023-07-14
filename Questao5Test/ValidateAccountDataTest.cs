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
    public class ValidateAccountDataTest
    {
        private readonly CreateMovementHandler _createMovementHandler;
        private readonly IMediator _mediator = Substitute.For<IMediator>();
        private readonly IAccountService _service = new AccountService();
        public ValidateAccountDataTest()
        {
            _createMovementHandler = new CreateMovementHandler(_mediator,_service);
        }

        [Fact]
        public async Task TestValidateWithoutValue()
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

            var result = await _createMovementHandler.Handle(new () {AccountNumber = "001", IdRequest = idRequestTest, TypeMovement = "C", Value = 0 }, new CancellationToken() { });
            var expectResult = new CreateMovementResponse() { Description = ErrorType.INVALID_VALUE.ToString(), IdMovimento = null, Result = 400 };
            Assert.Equal(expectResult.Description, result.Description);

        }

        [Fact]
        public async Task TestValidateAccount()
        {
            const string idRequestTest = "4f20ca9c-21e5-11ee-be56-0242ac120002";
            const string IdContaCorrentTest = "41d3539a-21e6-11ee-be56-0242ac120002";
            const string Ativo = "1";
            const string Nome = "Franccesco Felipe";

            _mediator.Send(Arg.Any<GetIdempotenciaQueryRequest>()).Returns(new GetIdempotenciaQueryResponse()
            {
                Chave_Idempotencia = "",
                Requisicao = "",
                Resultado = ""
            });

            GetIdAccountFindByNumberQueryRequest accountTest = null;
            _mediator.Send(Arg.Any<GetIdAccountFindByNumberQueryRequest>()).Returns(new GetIdAccountFindByNumberQueryResponse()
            {
                IdContaCorrente = null,
                Ativo = null,
                Nome = null,
                Numero = 0
            });

            var result = await _createMovementHandler.Handle(new() { AccountNumber = IdContaCorrentTest, IdRequest = idRequestTest, TypeMovement = "C", Value = 0 }, new CancellationToken() { });

            var expectResult = new CreateMovementResponse() { Description = ErrorType.INVALID_ACCOUNT.ToString(), IdMovimento = null, Result = 400 };

            Assert.Equal(expectResult.Description, result.Description);

        }

        [Fact]
        public async Task TestValidateActiveAccount()
        {
            const string idRequestTest = "4f20ca9c-21e5-11ee-be56-0242ac120002";
            const string IdContaCorrentTest = "41d3539a-21e6-11ee-be56-0242ac120002";
            const string Ativo = "0";
            const string Nome = "Franccesco Felipe";

            _mediator.Send(Arg.Any<GetIdempotenciaQueryRequest>()).Returns(new GetIdempotenciaQueryResponse()
            {
                Chave_Idempotencia = "",
                Requisicao = "",
                Resultado = ""
            });

            _mediator.Send(Arg.Any<GetIdAccountFindByNumberQueryRequest>()).Returns(new GetIdAccountFindByNumberQueryResponse()
            {
                IdContaCorrente = IdContaCorrentTest,
                Ativo = Ativo,
                Nome = Nome,
                Numero = 121
            });

            var result = await _createMovementHandler.Handle(new() { AccountNumber = "001", IdRequest = idRequestTest, TypeMovement = "C", Value = 0 }, new CancellationToken() { });
            var expectResult = new CreateMovementResponse() { Description = ErrorType.INACTIVE_ACCOUNT.ToString(), IdMovimento = null, Result = 400 };
            Assert.Equal(expectResult.Description, result.Description);

        }
    }
}

namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class GetIdempotenciaQueryResponse
    {
        public string Chave_Idempotencia { get; set; }
        public string Requisicao { get; set; }
        public string Resultado { get; set; }

    }
}

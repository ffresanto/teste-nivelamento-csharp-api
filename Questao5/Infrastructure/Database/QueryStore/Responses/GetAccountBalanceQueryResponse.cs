namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class GetAccountBalanceQueryResponse
    {
        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
    }
}

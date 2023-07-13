namespace Questao5.Infrastructure.Database.QueryStore.Responses
{
    public class GetIdAccountFindByNumberQueryResponse
    {
        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
    }
}

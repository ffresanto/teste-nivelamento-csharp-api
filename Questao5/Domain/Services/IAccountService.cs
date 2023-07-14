using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Domain.Services
{
    public interface IAccountService
    {
        public void ValidateAccount(GetIdAccountFindByNumberQueryResponse accountData);
        public void ValidateValue(decimal value);

    }
}

using Questao5.Domain.Enumerators;
using Questao5.Domain.Services;
using Questao5.Infrastructure.Database.QueryStore.Responses;

namespace Questao5.Application.Services
{
    public class AccountService : IAccountService
    {
        public void ValidateAccount(GetIdAccountFindByNumberQueryResponse accountData)
        {
            if (accountData == null)
                throw new Exception(ErrorType.INVALID_ACCOUNT.ToString());

            if (accountData.Ativo == "0")
                throw new Exception(ErrorType.INACTIVE_ACCOUNT.ToString());
        }

        public void ValidateValue(decimal value)
        {
            if (value <= 0)
                throw new Exception(ErrorType.INVALID_VALUE.ToString());
        }
    }
}

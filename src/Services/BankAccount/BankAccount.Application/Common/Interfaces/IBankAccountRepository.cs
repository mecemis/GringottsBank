using System;
using System.Threading.Tasks;

namespace BankAccount.Application.Common.Interfaces
{
    public interface IBankAccountRepository : IRepositoryBase<Domain.Aggregates.BankAccountAggregate.BankAccount>
    {
        Task<Domain.Aggregates.BankAccountAggregate.BankAccount> GetBankAccountTransactionsByDate(Guid customerId,
            int accountId, DateTime startDate, DateTime endDate);
    }
}
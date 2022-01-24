using System;
using System.Linq;
using System.Threading.Tasks;
using BankAccount.Application.Common.Interfaces;
using BankAccount.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Infrastructure.Repositories
{
    public class BankAccountRepository : RepositoryBase<Domain.Aggregates.BankAccountAggregate.BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(BankAccountDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Domain.Aggregates.BankAccountAggregate.BankAccount> GetBankAccountTransactionsByDate(Guid customerId, int accountId, DateTime startDate, DateTime endDate)
        {
            var account = _dbContext.BankAccounts.Where(x => x.CustomerId == customerId && x.Id == accountId)
                .Include(x =>
                    x.BankAccountTransactions.Where(t => t.CreatedDate >= startDate && t.CreatedDate <= endDate));

            return await account.FirstOrDefaultAsync();
        }
    }
}
using BankAccount.Domain.Common;
using BankAccount.Domain.Enums;

namespace BankAccount.Domain.Aggregates.BankAccountAggregate
{
    public class BankAccountTransaction : EntityBase
    {
        public BankAccount BankAccount { get; private set; }
        public int BankAccountId { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public decimal Amount { get; private set; }

        public BankAccountTransaction()
        {
        }

        public BankAccountTransaction(int bankAccountId, TransactionType transactionType, decimal amount)
        {
            BankAccountId = bankAccountId;
            TransactionType = transactionType;
            Amount = amount;
        }
    }
}
using BankAccount.Domain.Common;
using BankAccount.Domain.Enums;

namespace BankAccount.Domain.Aggregates.BankAccountAggregate
{
    public class BankAccountTransaction : EntityBase
    {
        public BankAccount BankAccount { get; set; }
        public int BankAccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
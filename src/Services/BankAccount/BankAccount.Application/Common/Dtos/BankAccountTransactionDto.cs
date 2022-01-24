using System;
using BankAccount.Domain.Enums;

namespace BankAccount.Application.Common.Dtos
{
    public class BankAccountTransactionDto
    {
        public int BankAccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
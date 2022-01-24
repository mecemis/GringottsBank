using System;
using System.Collections.Generic;

namespace BankAccount.Application.Common.Dtos
{
    public class BankAccountDetailDto
    {
        public int Id { get; set; }
        public Guid CustomerId { get;  set; }
        public List<BankAccountTransactionDto> BankAccountTransactions { get; set; }
        public DateTime CreatedDate { get; set; }

        public decimal Balance { get;  set; }

        public BankAccountDetailDto()
        {
            BankAccountTransactions = new List<BankAccountTransactionDto>();
        }
    }
}
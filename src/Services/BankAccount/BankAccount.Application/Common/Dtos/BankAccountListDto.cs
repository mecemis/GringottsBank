using System;

namespace BankAccount.Application.Common.Dtos
{
    public class BankAccountListDto
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }

        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
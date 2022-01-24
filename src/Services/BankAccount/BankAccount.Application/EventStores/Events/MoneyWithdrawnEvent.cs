using System;

namespace BankAccount.Application.EventStores.Events
{
    public class MoneyWithdrawnEvent : IEvent
    {
        public int BankAccountId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal DepositAmount { get; set; }
    }
}
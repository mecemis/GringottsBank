using System;

namespace BankAccount.Application.Events
{
    public class BankAccountCreatedEvent : IEvent
    {
        public Guid CustomerId { get; set; }
    }
}
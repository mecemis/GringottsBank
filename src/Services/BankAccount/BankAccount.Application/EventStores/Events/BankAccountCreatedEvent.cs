using System;

namespace BankAccount.Application.EventStores.Events
{
    public class BankAccountCreatedEvent : IEvent
    {
        public Guid CustomerId { get; set; }
    }
}
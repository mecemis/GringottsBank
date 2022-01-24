using BankAccount.Application.EventStores.Events;
using EventStore.ClientAPI;

namespace BankAccount.Application.EventStores
{
    public class BankAccountStream : AbstractStream
    {
        public const string StreamName = "BankAccountStream3";
        public const string GroupName = "group1";

        public BankAccountStream(IEventStoreConnection eventStoreConnection) : base(eventStoreConnection,
            StreamName)
        {
        }

        public void Deposited(MoneyDepositedEvent moneyDepositedEvent)
        {
            Events.AddLast(moneyDepositedEvent);
        }

        public void Withdrawn(MoneyWithdrawnEvent moneyWithdrawnEvent)
        {
            Events.AddLast(moneyWithdrawnEvent);
        }

        public void Created(BankAccountCreatedEvent bankAccountCreatedEvent)
        {
            Events.AddLast(bankAccountCreatedEvent);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankAccount.Application.EventStores.Events;
using EventStore.ClientAPI;

namespace BankAccount.Application.EventStores
{
    public abstract class AbstractStream
    {
        protected readonly LinkedList<IEvent> Events = new LinkedList<IEvent>();
        private readonly string _streamName;
        private readonly IEventStoreConnection _eventStoreConnection;

        protected AbstractStream(IEventStoreConnection eventStoreConnection, string streamName)
        {
            _eventStoreConnection = eventStoreConnection;
            _streamName = streamName;
        }

        public async Task SaveAsync()
        {
            var newEvents = Events.ToList().Select(x => new EventData(Guid.NewGuid(), x.GetType().Name, true,
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(x, inputType: x.GetType())),
                Encoding.UTF8.GetBytes(x.GetType().FullName))).ToList();

            await _eventStoreConnection.AppendToStreamAsync(_streamName, ExpectedVersion.Any, newEvents);

            Events.Clear();
        }
    }
}
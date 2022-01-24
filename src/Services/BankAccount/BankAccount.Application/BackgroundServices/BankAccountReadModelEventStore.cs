using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Application.Common.Interfaces;
using BankAccount.Application.Common.Settings;
using BankAccount.Application.EventStores;
using BankAccount.Application.EventStores.Events;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BankAccount.Application.BackgroundServices
{
    public class BankAccountReadModelEventStore : BackgroundService
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly ILogger<BankAccountReadModelEventStore> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<EventStoreSettings> _options;
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountReadModelEventStore(IEventStoreConnection eventStoreConnection,
            ILogger<BankAccountReadModelEventStore> logger, IServiceProvider serviceProvider, IOptions<EventStoreSettings> options, IBankAccountRepository bankAccountRepository)
        {
            _eventStoreConnection = eventStoreConnection;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _options = options;
            _bankAccountRepository = bankAccountRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            await _eventStoreConnection.CreatePersistentSubscriptionAsync(BankAccountStream.StreamName,
                BankAccountStream.GroupName, PersistentSubscriptionSettings.Create()
                    .StartFromBeginning().MinimumCheckPointCountOf(1).MaximumCheckPointCountOf(10), new UserCredentials("admin", "changeit"));

            await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(BankAccountStream.StreamName,
                BankAccountStream.GroupName, EventAppeared, autoAck: false);
        }

        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2)
        {
           
            var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata)}, BankAccount.Application");
            
            _logger.LogInformation($"The Message processing.. : {type.ToString()}");

            var eventData = Encoding.UTF8.GetString(arg2.Event.Data);

            var @event = JsonSerializer.Deserialize(eventData, type);

            //using var scope = _serviceProvider.CreateScope();

            //var context = scope.ServiceProvider.GetRequiredService<BankAccountDbContext>();

            Domain.Aggregates.BankAccountAggregate.BankAccount bankAccount = null;

            switch (@event)
            {
                case BankAccountCreatedEvent bankAccountCreatedEvent:

                    bankAccount = new(bankAccountCreatedEvent.CustomerId);
                    context.BankAccounts.Add(bankAccount);
                    break;

                case MoneyDepositedEvent moneyDepositedEvent:

                    bankAccount = context.BankAccounts.FirstOrDefault(x=>x.Id==moneyDepositedEvent.BankAccountId&&x.CustomerId==moneyDepositedEvent.CustomerId);

                    bankAccount?.DepositMoney(moneyDepositedEvent.DepositAmount);

                    break;

                case MoneyWithdrawnEvent moneyWithdrawnEvent:

                    bankAccount = context.BankAccounts.FirstOrDefault(x => x.Id == moneyWithdrawnEvent.BankAccountId && x.CustomerId == moneyWithdrawnEvent.CustomerId);

                    bankAccount?.WithdrawMoney(moneyWithdrawnEvent.DepositAmount);

                    break;
            }

            await context.SaveChangesAsync();
            
            arg1.Acknowledge(arg2.Event.EventId);
        }
    }
}
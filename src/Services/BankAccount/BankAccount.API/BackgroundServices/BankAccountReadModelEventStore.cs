using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Application.Events;
using BankAccount.Infrastructure.EventStores;
using BankAccount.Infrastructure.Persistence;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BankAccount.API.BackgroundServices
{
    public class BankAccountReadModelEventStore : BackgroundService
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly ILogger<BankAccountReadModelEventStore> _logger;
        private readonly IServiceProvider _serviceProvider;

        public BankAccountReadModelEventStore(IEventStoreConnection eventStoreConnection,
            ILogger<BankAccountReadModelEventStore> logger, IServiceProvider serviceProvider)
        {
            _eventStoreConnection = eventStoreConnection;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(BankAccountStream.StreamName,
                BankAccountStream.GroupName, EventAppeared, autoAck: false);
            //autoAck true:  EventAppeared exception firlamadı ise event gönderildi sayar.
            //false : 
        }

        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2)
        {
           
            var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata)}, BankAccount.Application");
            
            _logger.LogInformation($"The Message processing.. : {type.ToString()}");

            var eventData = Encoding.UTF8.GetString(arg2.Event.Data);

            var @event = JsonSerializer.Deserialize(eventData, type);

            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BankAccountDbContext>();

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
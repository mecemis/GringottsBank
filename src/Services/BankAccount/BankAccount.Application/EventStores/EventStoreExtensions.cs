using System;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BankAccount.Application.EventStores
{
    public static class EventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionSettings = ConnectionSettings.Create();
            connectionSettings.EnableVerboseLogging()
                .UseDebugLogger()
                .UseConsoleLogger()
                .SetHeartbeatTimeout(TimeSpan.FromSeconds(60))
                .SetHeartbeatInterval(TimeSpan.FromSeconds(30)).DisableTls();

            var connection =
                EventStoreConnection.Create(connectionString: configuration.GetConnectionString("EventStore"), connectionSettings);

            // var connection =
            //     EventStoreConnection.Create(connectionString: configuration.GetConnectionString("EventStore"));

            connection.ConnectAsync().Wait();

            services.AddSingleton(connection);

            using var logFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddConsole();
            });

            var logger = logFactory.CreateLogger("Startup");

            connection.Connected += (sender, args) =>
            {
                logger.LogInformation("EventStore connection established");
            };

            connection.ErrorOccurred += (sender, args) =>
            {
                logger.LogError(args.Exception.Message);
            };
        }
    }
}
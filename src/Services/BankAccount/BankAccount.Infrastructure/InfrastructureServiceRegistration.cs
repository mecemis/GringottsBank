using BankAccount.Application.Common.Interfaces;
using BankAccount.Infrastructure.Persistence;
using BankAccount.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccount.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BankAccountDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BankAccountDb")));
            
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            return services;
        }
    }
}
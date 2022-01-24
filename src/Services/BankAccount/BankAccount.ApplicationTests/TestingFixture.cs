using BankAccount.Application.Common.Behaviors;
using BankAccount.Application.Common.Interfaces;
using BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount;
using BankAccount.Infrastructure.Persistence;
using BankAccount.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BankAccount.ApplicationTests
{
    public class TestingFixture : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        public TestingFixture()
        {
            _serviceProvider = new ServiceCollection()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly()).AddMediatR(typeof(CreateBankAccountCommandHandler))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddDbContext<BankAccountDbContext>(options =>
                options.UseInMemoryDatabase("BankAccountDb"))
                .AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))
                .AddScoped<IBankAccountRepository, BankAccountRepository>().AddLogging()
                .BuildServiceProvider();

            var context = _serviceProvider.GetRequiredService<BankAccountDbContext>();

            //context.Database.Migrate();
            
            context.BankAccounts.Add(new Domain.Aggregates.BankAccountAggregate.BankAccount(Guid.Parse(TestConstants.TestCustomerGuid)));

            context.SaveChanges();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var mediator = _serviceProvider.GetRequiredService<IMediator>();

            return await mediator.Send(request);
        }

        public async Task<List<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
            where TEntity : class
        {

            var context = _serviceProvider.GetRequiredService<BankAccountDbContext>();

            IQueryable<TEntity> query = context.Set<TEntity>();

            if (predicate != null) query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public void Dispose()
        {
            
        }
    }
}
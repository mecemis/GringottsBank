using System;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Infrastructure.Persistence
{
    public class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext(DbContextOptions<BankAccountDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Aggregates.BankAccountAggregate.BankAccount> BankAccounts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
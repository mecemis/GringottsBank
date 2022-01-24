using System;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Domain.Aggregates.BankAccountAggregate;
using BankAccount.Domain.Common;
using GringottsBank.Shared.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Infrastructure.Persistence
{
    public class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext(DbContextOptions<BankAccountDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Aggregates.BankAccountAggregate.BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountTransaction> BankAccountTransactions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = CurrentCustomer.FullName;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = CurrentCustomer.FullName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Aggregates.BankAccountAggregate.BankAccount>().ToTable("BankAccounts");
            modelBuilder.Entity<Domain.Aggregates.BankAccountAggregate.BankAccountTransaction>().ToTable("BankAccountTransactions");

            modelBuilder.Entity<Domain.Aggregates.BankAccountAggregate.BankAccount>()
                .HasMany(x => x.BankAccountTransactions).WithOne(x => x.BankAccount);
        }
    }
}
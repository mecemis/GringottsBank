using System;
using BankAccount.Domain.Exceptions;
using Xunit;

namespace BankAccount.Domain.Tests
{
    public class BankAccountTests
    {

        [Fact]
        public void Should_Deposit_Successfully()
        {
            Aggregates.BankAccountAggregate.BankAccount bankAccount =
                new Aggregates.BankAccountAggregate.BankAccount(Guid.NewGuid());

            bankAccount.DepositMoney(5);

            Assert.Equal(5, bankAccount.Balance);
            Assert.NotEmpty(bankAccount.BankAccountTransactions);
        }

        [Fact]
        public void Should_Throw_InvalidDepositAmount_Exception()
        {
            Aggregates.BankAccountAggregate.BankAccount bankAccount =
                new Aggregates.BankAccountAggregate.BankAccount(Guid.NewGuid());

            Assert.Throws<InvalidDepositAmountException>(()=>bankAccount.DepositMoney(-5));
            Assert.Empty(bankAccount.BankAccountTransactions);
        }

        [Fact]
        public void Should_Withdraw_Successfully()
        {
            Aggregates.BankAccountAggregate.BankAccount bankAccount =
                new Aggregates.BankAccountAggregate.BankAccount(Guid.NewGuid());

            bankAccount.DepositMoney(5);
            bankAccount.WithdrawMoney(4);

            Assert.Equal(1, bankAccount.Balance);
            Assert.NotEmpty(bankAccount.BankAccountTransactions);
        }

        [Fact]
        public void Should_Throw_InvalidWithdrawAmount_Exception()
        {
            Aggregates.BankAccountAggregate.BankAccount bankAccount =
                new Aggregates.BankAccountAggregate.BankAccount(Guid.NewGuid());

            Assert.Throws<InvalidWithdrawAmountException>(() => bankAccount.WithdrawMoney(5));
            Assert.Empty(bankAccount.BankAccountTransactions);
        }
    }
}
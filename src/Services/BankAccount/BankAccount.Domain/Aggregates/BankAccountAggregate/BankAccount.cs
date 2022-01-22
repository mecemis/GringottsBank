﻿using System;
using System.Collections.Generic;
using BankAccount.Domain.Common;
using BankAccount.Domain.Enums;
using BankAccount.Domain.Exceptions;

namespace BankAccount.Domain.Aggregates.BankAccountAggregate
{
    public class BankAccount : EntityBase
    {
        public Guid CustomerId { get; private set; }
        public ICollection<BankAccountTransaction> BankAccountTransactions { get; private set; }

        public decimal Balance { get; private set; }

        public BankAccount(Guid customerId)
        {
            BankAccountTransactions = new List<BankAccountTransaction>();
            CustomerId = customerId;
        }

        public void DepositMoney(decimal amount)
        {
            if (amount <= 0) 
                throw new InvalidDepositAmountException();

            Balance += amount;

            var transaction = new BankAccountTransaction()
            {
                Amount = amount,
                BankAccountId = Id,
                TransactionType = TransactionType.Deposit
            };

            BankAccountTransactions.Add(transaction);
        }

        public void WithdrawMoney(decimal amount)
        {
            if (amount <= 0 || amount > Balance) 
                throw new InvalidWithdrawAmountException();

            Balance -= amount;

            var transaction = new BankAccountTransaction()
            {
                Amount = amount,
                BankAccountId = Id,
                TransactionType = TransactionType.Withdraw
            };

            BankAccountTransactions.Add(transaction);
        }
    }
}
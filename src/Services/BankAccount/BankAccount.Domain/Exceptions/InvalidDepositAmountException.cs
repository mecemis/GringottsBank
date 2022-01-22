using System;

namespace BankAccount.Domain.Exceptions
{
    public class InvalidDepositAmountException : Exception
    {
        public InvalidDepositAmountException() : base("The amount you want to deposit is invalid")
        {
        }
    }
}
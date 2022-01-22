using System;

namespace BankAccount.Domain.Exceptions
{
    public class InvalidWithdrawAmountException : Exception
    {
        public InvalidWithdrawAmountException() : base("The amount you want to withdraw is invalid")
        {
        }
    }
}
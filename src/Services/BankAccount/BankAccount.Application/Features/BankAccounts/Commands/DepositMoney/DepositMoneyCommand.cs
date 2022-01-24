using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Commands.DepositMoney
{
    public class DepositMoneyCommand : IRequest<Response<bool>>
    {
        public int BankAccountId { get; set; }
        public decimal DepositAmount { get; set; }
    }
}
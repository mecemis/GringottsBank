using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommand : IRequest<Response<bool>>
    {
        public int BankAccountId { get; set; }
        public decimal WithdrawAmount { get; set; }
    }
}
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount
{
    public class CreateBankAccountCommand : IRequest<Response<bool>>
    {
        
    }
}
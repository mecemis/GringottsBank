using BankAccount.Application.Common.Dtos;
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetail
{
    public class GetAccountDetailsQuery : IRequest<Response<BankAccountDetailDto>>
    {
        public int AccountId { get; set; }
    }
}
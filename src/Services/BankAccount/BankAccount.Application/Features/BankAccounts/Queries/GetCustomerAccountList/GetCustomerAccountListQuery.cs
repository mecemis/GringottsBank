using System.Collections.Generic;
using BankAccount.Application.Common.Dtos;
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetCustomerAccountList
{
    public class GetCustomerAccountListQuery : IRequest<Response<IReadOnlyList<BankAccountListDto>>>
    {
    }
}
using System;
using BankAccount.Application.Common.Dtos;
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetailsByDate
{
    public class GetAccountDetailsByDateQuery : IRequest<Response<BankAccountDetailDto>>
    {
        public int AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
using AutoMapper;
using BankAccount.Application.Common.Dtos;
using BankAccount.Domain.Aggregates.BankAccountAggregate;

namespace BankAccount.Application.Common.Mappings
{
    public class BankAccountMapping : Profile
    {
        public BankAccountMapping()
        {
            CreateMap<Domain.Aggregates.BankAccountAggregate.BankAccount, BankAccountDetailDto>();
            CreateMap<BankAccountTransaction, BankAccountTransactionDto>();

            CreateMap<Domain.Aggregates.BankAccountAggregate.BankAccount, BankAccountListDto>();
        }
    }
}
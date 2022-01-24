using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BankAccount.Application.Common.Dtos;
using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetCustomerAccountList
{
    public class GetCustomerAccountListQueryHandler : IRequestHandler<GetCustomerAccountListQuery, Response<IReadOnlyList<BankAccountListDto>>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetCustomerAccountListQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<BankAccountListDto>>> Handle(GetCustomerAccountListQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Domain.Aggregates.BankAccountAggregate.BankAccount> bankAccountList = await _bankAccountRepository.GetAllAsync(x => x.CustomerId == CurrentCustomer.Id);

            IReadOnlyList<BankAccountListDto> accountListDto = _mapper.Map<IReadOnlyList<BankAccountListDto>>(bankAccountList);

            return Response<IReadOnlyList<BankAccountListDto>>.Success(accountListDto, 200);
        }
    }
}
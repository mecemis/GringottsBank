using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BankAccount.Application.Common.Dtos;
using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetail
{
    public class GetAccountDetailsQueryHandler : IRequestHandler<GetAccountDetailsQuery, Response<BankAccountDetailDto>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetAccountDetailsQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<Response<BankAccountDetailDto>> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            Domain.Aggregates.BankAccountAggregate.BankAccount bankAccount = await _bankAccountRepository.GetAsync(x => x.Id == request.AccountId && x.CustomerId == CurrentCustomer.Id, includeString: "BankAccountTransactions");

            BankAccountDetailDto accountDetailDto = _mapper.Map<BankAccountDetailDto>(bankAccount);

            return Response<BankAccountDetailDto>.Success(accountDetailDto, 200);
        }
    }
}
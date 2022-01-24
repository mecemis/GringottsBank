using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BankAccount.Application.Common.Dtos;
using BankAccount.Application.Common.Interfaces;
using BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetail;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetailsByDate
{
    public class GetAccountDetailsByDateQueryHandler : IRequestHandler<GetAccountDetailsByDateQuery, Response<BankAccountDetailDto>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetAccountDetailsByDateQueryHandler(IBankAccountRepository bankAccountRepository, IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<Response<BankAccountDetailDto>> Handle(GetAccountDetailsByDateQuery request, CancellationToken cancellationToken)
        {
            Domain.Aggregates.BankAccountAggregate.BankAccount bankAccount = await _bankAccountRepository.GetBankAccountTransactionsByDate(CurrentCustomer.Id, request.AccountId, request.StartDate, request.EndDate);

            BankAccountDetailDto accountDetailDto = _mapper.Map<BankAccountDetailDto>(bankAccount);

            return Response<BankAccountDetailDto>.Success(accountDetailDto, 200);
        }
    }
}
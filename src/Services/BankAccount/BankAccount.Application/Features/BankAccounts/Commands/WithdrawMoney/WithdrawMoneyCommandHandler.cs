using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Application.EventStores;
using BankAccount.Application.EventStores.Events;

namespace BankAccount.Application.Features.BankAccounts.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommandHandler : IRequestHandler<WithdrawMoneyCommand, Response<bool>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly BankAccountStream _bankAccountStream;

        public WithdrawMoneyCommandHandler(IBankAccountRepository bankAccountRepository, BankAccountStream bankAccountStream)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankAccountStream = bankAccountStream;
        }

        public async Task<Response<bool>> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
        {
            _bankAccountStream.Withdrawn(new MoneyWithdrawnEvent() { CustomerId = CurrentCustomer.Id, BankAccountId = request.BankAccountId, DepositAmount = request.WithdrawAmount });

            await _bankAccountStream.SaveAsync();

            return Response<bool>.Success(200);
        }
    }
}
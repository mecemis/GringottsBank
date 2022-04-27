using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Application.EventStores;
using BankAccount.Application.EventStores.Events;

namespace BankAccount.Application.Features.BankAccounts.Commands.DepositMoney
{
    public class DepositMoneyCommandHandler : IRequestHandler<DepositMoneyCommand, Response<bool>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly BankAccountStream _bankAccountStream;

        public DepositMoneyCommandHandler(IBankAccountRepository bankAccountRepository, BankAccountStream bankAccountStream)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankAccountStream = bankAccountStream;
        }

        public async Task<Response<bool>> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
        {
            _bankAccountStream.Deposited(new MoneyDepositedEvent() { CustomerId = CurrentCustomer.Id, BankAccountId = request.BankAccountId, DepositAmount = request.DepositAmount});

            await _bankAccountStream.SaveAsync();
            
            return Response<bool>.Success(200);
        }
    }
}
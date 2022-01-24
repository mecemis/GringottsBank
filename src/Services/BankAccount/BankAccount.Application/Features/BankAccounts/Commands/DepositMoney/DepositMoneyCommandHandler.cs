using BankAccount.Application.EventStores;
using GringottsBank.Shared.Models.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Application.EventStores.Events;
using GringottsBank.Shared.Models.Responses;

namespace BankAccount.Application.Features.BankAccounts.Commands.DepositMoney
{
    public class DepositMoneyCommandHandler : IRequestHandler<DepositMoneyCommand, Response<bool>>
    {
        private readonly BankAccountStream _bankAccountStream;

        public DepositMoneyCommandHandler(BankAccountStream bankAccountStream)
        {
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
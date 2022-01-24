using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Application.Features.BankAccounts.Commands.WithdrawMoney
{
    public class WithdrawMoneyCommandHandler : IRequestHandler<WithdrawMoneyCommand, Response<bool>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public WithdrawMoneyCommandHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<Response<bool>> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
        {
            //_bankAccountStream.Withdrawn(new MoneyWithdrawnEvent() { CustomerId = CurrentCustomer.Id, BankAccountId = request.BankAccountId, DepositAmount = request.WithdrawAmount });

            //await _bankAccountStream.SaveAsync();

            Domain.Aggregates.BankAccountAggregate.BankAccount bankAccount = await _bankAccountRepository.GetAsync(x => x.Id == request.BankAccountId && x.CustomerId == CurrentCustomer.Id);

            if (bankAccount is null)
                return Response<bool>.Fail("Account not exist", 400);

            bankAccount.WithdrawMoney(request.WithdrawAmount);

            await _bankAccountRepository.UpdateAsync(bankAccount);

            return Response<bool>.Success(200);
        }
    }
}
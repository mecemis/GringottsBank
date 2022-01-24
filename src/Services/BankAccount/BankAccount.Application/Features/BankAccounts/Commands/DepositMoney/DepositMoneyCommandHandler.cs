using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Application.Features.BankAccounts.Commands.DepositMoney
{
    public class DepositMoneyCommandHandler : IRequestHandler<DepositMoneyCommand, Response<bool>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public DepositMoneyCommandHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<Response<bool>> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
        {
            #region SadStory

            //_bankAccountStream.Deposited(new MoneyDepositedEvent() { CustomerId = CurrentCustomer.Id, BankAccountId = request.BankAccountId, DepositAmount = request.DepositAmount});

            //await _bankAccountStream.SaveAsync();

            #endregion

            Domain.Aggregates.BankAccountAggregate.BankAccount bankAccount = await _bankAccountRepository.GetAsync(x => x.Id == request.BankAccountId && x.CustomerId == CurrentCustomer.Id);

            if (bankAccount is null)
                return Response<bool>.Fail("Account not exist", 400);

            bankAccount.DepositMoney(request.DepositAmount);

            await _bankAccountRepository.UpdateAsync(bankAccount);

         
            return Response<bool>.Success(200);
        }
    }
}
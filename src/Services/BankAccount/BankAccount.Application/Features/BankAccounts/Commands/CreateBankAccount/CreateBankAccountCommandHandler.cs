using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Response<bool>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        public CreateBankAccountCommandHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<Response<bool>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            #region EventStore

            //_bankAccountStream.Created(new BankAccountCreatedEvent(){CustomerId = CurrentCustomer.Id });

            //await _bankAccountStream.SaveAsync();

            #endregion

            await _bankAccountRepository.AddAsync(
                new Domain.Aggregates.BankAccountAggregate.BankAccount(CurrentCustomer.Id));

            return Response<bool>.Success(200);
        }
    }
}
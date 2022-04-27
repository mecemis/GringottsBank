using BankAccount.Application.Common.Interfaces;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BankAccount.Application.EventStores;
using BankAccount.Application.EventStores.Events;
//using BankAccount = BankAccount.Domain.Aggregates.BankAccountAggregate.BankAccount;

namespace BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Response<bool>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly BankAccountStream _bankAccountStream;
        public CreateBankAccountCommandHandler(IBankAccountRepository bankAccountRepository, BankAccountStream bankAccountStream)
        {
            _bankAccountRepository = bankAccountRepository;
            _bankAccountStream = bankAccountStream;
        }

        public async Task<Response<bool>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            _bankAccountStream.Created(new BankAccountCreatedEvent(){CustomerId = CurrentCustomer.Id });

            //_bankAccountRepository.AddAsync(new Domain.Aggregates.BankAccountAggregate.BankAccount(CurrentCustomer.Id));

            await _bankAccountStream.SaveAsync();

            return Response<bool>.Success(200);
        }
    }
}
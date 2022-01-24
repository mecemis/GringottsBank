using System.Threading;
using System.Threading.Tasks;
using BankAccount.Application.EventStores;
using BankAccount.Application.EventStores.Events;
using GringottsBank.Shared.Models.Identity;
using GringottsBank.Shared.Models.Responses;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, Response<bool>>
    {
        private readonly BankAccountStream _bankAccountStream;
        public CreateBankAccountCommandHandler(BankAccountStream bankAccountStream)
        {
            _bankAccountStream = bankAccountStream;
        }

        public async Task<Response<bool>> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            _bankAccountStream.Created(new BankAccountCreatedEvent(){CustomerId = CurrentCustomer.Id });

            await _bankAccountStream.SaveAsync();

            return Response<bool>.Success(200);
        }
    }
}
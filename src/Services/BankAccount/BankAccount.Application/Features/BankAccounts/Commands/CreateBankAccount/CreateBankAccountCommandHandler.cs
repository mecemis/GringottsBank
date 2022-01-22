using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand>
    {
        public Task<Unit> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
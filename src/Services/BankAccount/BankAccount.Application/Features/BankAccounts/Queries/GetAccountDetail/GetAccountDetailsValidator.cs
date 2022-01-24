using FluentValidation;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetail
{
    public class GetAccountDetailsValidator : AbstractValidator<GetAccountDetailsQuery>
    {
        public GetAccountDetailsValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0).WithMessage("InvalidAccountNumber");
        }
    }
}
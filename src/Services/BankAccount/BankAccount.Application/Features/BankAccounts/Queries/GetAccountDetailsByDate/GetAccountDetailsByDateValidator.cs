using FluentValidation;

namespace BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetailsByDate
{
    public class GetAccountDetailsByDateValidator : AbstractValidator<GetAccountDetailsByDateQuery>
    {
        public GetAccountDetailsByDateValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0).WithMessage("InvalidAccountNumber");
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
            RuleFor(x => x.EndDate).GreaterThan(x=>x.StartDate).WithMessage("{StartDate} can not be bigger than {EndDate}");
        }
    }
}
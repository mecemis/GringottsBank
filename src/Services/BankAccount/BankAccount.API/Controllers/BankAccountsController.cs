using BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount;
using BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using BankAccount.Application.Features.BankAccounts.Queries.GetAccountDetailsByDate;
using BankAccount.Application.Features.BankAccounts.Queries.GetCustomerAccountList;
using GringottsBank.Shared.ControllerBases;

namespace BankAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public BankAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-bank-account")]
        public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        [HttpGet("get-customer-account-details")]
        public async Task<IActionResult> GetAccountDetails([FromQuery] GetAccountDetailsQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateActionResult(result);
        }

        [HttpGet("get-customer-account-details-by-date")]
        public async Task<IActionResult> GetAccountDetailsByDate([FromQuery] GetAccountDetailsByDateQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateActionResult(result);
        }

        [HttpGet("get-customer-account-list")]
        public async Task<IActionResult> GetAccountList([FromQuery] GetCustomerAccountListQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateActionResult(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BankAccount.Application.Features.BankAccounts.Commands.DepositMoney;
using BankAccount.Application.Features.BankAccounts.Commands.WithdrawMoney;
using GringottsBank.Shared.ControllerBases;
using MediatR;

namespace BankAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("deposit-money")]
        public async Task<IActionResult> DepositMoney([FromBody] DepositMoneyCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }

        [HttpPost("withdraw-money")]
        public async Task<IActionResult> WithdrawMoney([FromBody] WithdrawMoneyCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateActionResult(result);
        }
    }
}

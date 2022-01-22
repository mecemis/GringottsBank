using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BankAccount.Application.Features.BankAccounts.Commands.CreateBankAccount;
using MediatR;

namespace BankAccount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "create-bank-account")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateBankAccount([FromBody] CreateBankAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GringottsBank.IdentityServer.Dtos;
using GringottsBank.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using GringottsBank.Shared.Models.Responses;

namespace GringottsBank.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = createCustomerDto.Username,
                Email = createCustomerDto.Email,
                IsActive = true, 
                FullName = createCustomerDto.FullName
            };

            IdentityResult result = await _userManager.CreateAsync(user, createCustomerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }

            return NoContent();
        }
    }
}

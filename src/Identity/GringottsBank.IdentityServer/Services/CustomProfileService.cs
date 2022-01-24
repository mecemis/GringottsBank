using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GringottsBank.IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace GringottsBank.IdentityServer.Services
{
    public class CustomProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
                new Claim("full_name", user.FullName),
                new Claim("username", user.UserName),
                new Claim("email", user.Email),
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = user is {IsActive: false};
        }
    }
}
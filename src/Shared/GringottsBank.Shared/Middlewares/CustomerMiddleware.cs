using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GringottsBank.Shared.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GringottsBank.Shared.Middlewares
{
    public class CustomerMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Constructor

        public CustomerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Methods

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                if (!string.IsNullOrEmpty(httpContext.Request.Headers["Authorization"]) && httpContext.Request.Headers["Authorization"].Any())
                {
                    var result = await httpContext.Request.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                    if (result.Succeeded)
                        httpContext.User = result.Principal;
                }

                var userClaims = httpContext.User?.Claims;

                var id = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

             
                if (!string.IsNullOrEmpty(id))/*(customerId > 0)*/
                {
                    CurrentCustomer.IsAuthenticated = true;
                    CurrentCustomer.Id = Guid.Parse(id);
                    CurrentCustomer.Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                    CurrentCustomer.UserName = userClaims.FirstOrDefault(x => x.Type == "username")?.Value;
                    CurrentCustomer.FullName = userClaims.FirstOrDefault(x => x.Type == "full_name")?.Value;
                }
            }
            catch
            {
            }

            await _next(httpContext);
        }

        #endregion
    }

    public static class CustomerMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomerMiddleware>();
        }
    }
}
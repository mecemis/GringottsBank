using GringottsBank.Shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Shared.ControllerBases
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
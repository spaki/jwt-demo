using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JWTDemo.API.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class RootController : ControllerBase
    {
        protected string GetClaim(string claimType) => User?.Claims?.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
}

using Microsoft.AspNetCore.Mvc;

namespace JWTDemo.API.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class RootController : ControllerBase
    {
        
    }
}

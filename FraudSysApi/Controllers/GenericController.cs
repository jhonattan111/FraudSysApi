using Microsoft.AspNetCore.Mvc;

namespace FraudSysApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericController : ControllerBase
    {
    }
}

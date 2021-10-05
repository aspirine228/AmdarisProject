using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GameTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController :ControllerBase
    {

    }
}

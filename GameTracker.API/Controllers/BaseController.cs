using GameTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace GameTracker.API.Controllers
{
   // [Authorize]
    [ApiController]
    public abstract class BaseController :ControllerBase
    {

    }
}

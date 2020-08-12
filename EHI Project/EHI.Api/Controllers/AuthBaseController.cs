using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthBaseController : ControllerBase
    {
        public Guid UserId { get; set; }
        public AuthBaseController(IHttpContextAccessor httpContextAccessor)
        {
            UserId = Guid.Parse(httpContextAccessor.HttpContext.User.Identity.Name);
        }
    }
}
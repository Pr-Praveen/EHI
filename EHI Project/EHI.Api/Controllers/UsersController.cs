using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHI.Api.Services.Auth;
using EHI.BLL.IBusinessComponent;
using EHI.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHI.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserAuthService _userAuthService = null;        
        public UsersController(IUserAuthService _userAuthService)
        {
            this._userAuthService = _userAuthService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserLoginInputModel model)
        {
            var response = await _userAuthService.Authenticate(model);
            if (response == null)
                return BadRequest(response);

            return  Ok(response);
        }


    }
}
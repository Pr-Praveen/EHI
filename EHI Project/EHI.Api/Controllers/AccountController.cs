using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHI.BLL.IBusinessComponent;
using EHI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserComponent _userComponent;
        public AccountController(IUserComponent userComponent)
        {
            _userComponent = userComponent;
        }

        [HttpPost("authenticate")]
        public UserModel Authenticate(UserModel bookViewModel)
        {
            return new UserModel
            {
                id = "123",
                username = "admin",
                password = "123",
                firstName = "Praveen",
                lastName = "Pandey",
                token = "string",
            };
        }

        [HttpGet("GetUser")]
        public async Task<UserViewModel> GetUser(UserViewModel model)
        {
            return await _userComponent.GetUser(model);
        }
       
    }
    
}

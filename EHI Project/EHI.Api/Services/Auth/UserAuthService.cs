using EHI.BLL.IBusinessComponent;
using EHI.Models;
using EHI.Models.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Api.Services.Auth
{
    public interface IUserAuthService
    {
        Task<UserAuthResponse> Authenticate(UserLoginInputModel model);
    }
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserComponent userComponent;        
        private readonly AppSettings _appSettings;

        public UserAuthService(IOptions<AppSettings> appSettings, IUserComponent userComponent)
        {
            _appSettings = appSettings.Value;
            this.userComponent = userComponent;
        }

        public async Task<UserAuthResponse> Authenticate(UserLoginInputModel model)
        {
            var user = await userComponent.GetUser(model.Username, model.Password);

            // return null if user not found
            if (user != null && user.Id != Guid.Empty)
            {
                var token = generateJwtToken(user);
                return new UserAuthResponse(user, token);
            }            
            return null;
        }

        private string generateJwtToken(UserViewModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

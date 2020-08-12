using System;
using System.Collections.Generic;
using System.Text;

namespace EHI.Models.Models
{
    public class UserAuthResponse
    {
        public UserAuthResponse(UserViewModel user, string token)
        {
            Id = user.Id;
            FirstName = user.firstName;
            LastName = user.lastName;
            Username = user.username;
            Token = token;
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


      
    }
}

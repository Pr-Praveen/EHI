using EHI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.DAL.IDataAccessRepository
{
    public interface IUserRepository
    {
        Task<UserViewModel>GetUser(UserViewModel model);
        Task<UserViewModel> GetUser(string userName, string pass);      
        
    }
}

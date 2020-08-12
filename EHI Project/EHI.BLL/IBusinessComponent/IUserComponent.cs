using EHI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.BLL.IBusinessComponent
{
    public interface IUserComponent
    {
        Task<UserViewModel> GetUser(UserViewModel model);
        Task<UserViewModel> GetUser(string userName, string pass);

    }
}

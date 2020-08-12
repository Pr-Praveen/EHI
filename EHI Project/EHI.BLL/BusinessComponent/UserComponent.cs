using EHI.BLL.IBusinessComponent;
using EHI.DAL.IDataAccessRepository;
using EHI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.BLL.BusinessComponent
{
    public class UserComponent : IUserComponent
    {
        private readonly IUserRepository _userRepository;
        public UserComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> GetUser(UserViewModel model)
        {
            return await _userRepository.GetUser(model);
        }
        public async Task<UserViewModel> GetUser(string userName, string pass)
        {
            return await _userRepository.GetUser(userName, pass);
        }

    }
}

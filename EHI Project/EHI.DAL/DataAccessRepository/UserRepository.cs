using EHI.DAL.DataModel;
using EHI.DAL.IDataAccessRepository;
using EHI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHI.DAL.DataAccessRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly EHIContext _dbContext = null;
        public UserRepository(EHIContext eHIContext)
        {
            _dbContext = eHIContext;
        }

      
        public async Task<UserViewModel> GetUser(UserViewModel model)
        {
            var result= await _dbContext.User.FirstOrDefaultAsync(a => a.Id == model.Id);
            UserViewModel usermodel = new UserViewModel();
            usermodel.Id = result.Id;
            usermodel.firstName = result.FirstName;
            usermodel.lastName = result.LastName;
            usermodel.username = result.UserName;
            usermodel.password = result.PassWord;
            return usermodel;
        }

        public async Task<UserViewModel> GetUser(string userName, string pass)
        {            
            UserViewModel usermodel = new UserViewModel();
            var result = await _dbContext.User.FirstOrDefaultAsync(a => a.UserName.Equals(userName) && a.PassWord.Equals(pass));
               if (result != null) {               
                usermodel.Id = result.Id;
                usermodel.firstName = result.FirstName;
                usermodel.lastName = result.LastName;
                usermodel.username = result.UserName;
            }
            
            return usermodel;
        }
    }
}

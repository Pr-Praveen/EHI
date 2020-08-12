using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHI.Api
{
    public class DependencyConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Services.Auth.IUserAuthService, Services.Auth.UserAuthService>();

            services.AddTransient<DAL.IDataAccessRepository.IUserRepository, DAL.DataAccessRepository.UserRepository>();
            services.AddTransient<BLL.IBusinessComponent.IUserComponent, BLL.BusinessComponent.UserComponent>();

            services.AddTransient<DAL.IDataAccessRepository.IContactsRepository, DAL.DataAccessRepository.ContactsRepository>();
            services.AddTransient<BLL.IBusinessComponent.IContactsComponent, BLL.BusinessComponent.ContactsComponent>();

        }
    }
}

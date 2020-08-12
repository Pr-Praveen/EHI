using EHI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.DAL.IDataAccessRepository
{
   public interface IContactsRepository
    {
        Task<IEnumerable<ContactViewModel>> GetAllContacts();
        Task<ContactViewModel> GetContact(Guid ContactId);
        Task<bool> AddContact(ContactViewModel model);
        Task<bool> UpdateContact(ContactViewModel model);
        Task<bool> UpdateContactStatus(Guid ContactId);

    }
}

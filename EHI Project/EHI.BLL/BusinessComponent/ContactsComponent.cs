using EHI.BLL.IBusinessComponent;
using EHI.DAL.IDataAccessRepository;
using EHI.Models;
using EHI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.BLL.BusinessComponent
{
    public class ContactsComponent : IContactsComponent
    {
        private readonly IContactsRepository _webSiteRepository;
        public ContactsComponent(IContactsRepository webSiteRepository)
        {
            _webSiteRepository = webSiteRepository;
        }

        public async Task<bool> AddContact(ContactViewModel model)
        {
            return await _webSiteRepository.AddContact(model);
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContacts()
        {
            return await _webSiteRepository.GetAllContacts();
        }

        public async Task<ContactViewModel> GetContact(Guid ContactId)
        {
            return await _webSiteRepository.GetContact(ContactId);
        }

        public async Task<bool> UpdateContact(ContactViewModel model)
        {
            return await _webSiteRepository.UpdateContact(model);
        }

        public async Task<bool> UpdateContactStatus(Guid ContactId)
        {
            return await _webSiteRepository.UpdateContactStatus(ContactId);
        }
    }
}

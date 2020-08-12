using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHI.BLL.IBusinessComponent;
using EHI.Models;
using EHI.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactsComponent  contactsComponent;      
        public ContactController(IContactsComponent contactsComponent) 
        {
            this.contactsComponent = contactsComponent;
        }

        [HttpPost("AddContact")]
        public async Task<bool> AddContact(ContactViewModel model)
        {
            return await contactsComponent.AddContact(model);
        }

        [HttpGet("GetAllContacts")]
        public async Task<IEnumerable<ContactViewModel>> GetAllContacts()
        {
            return await contactsComponent.GetAllContacts();
        }

        [HttpGet("GetContact")]
        public async Task<ContactViewModel> GetContact(Guid ContactId)
        {
            return await contactsComponent.GetContact(ContactId);
        }

        [HttpPost("UpdateContact")]
        public async Task<bool> UpdateContact(ContactViewModel model)
        {
            return await contactsComponent.UpdateContact(model);
        }

        [HttpGet("UpdateContactStatus")]
        public async Task<bool> UpdateContactStatus(Guid ContactId)
        {
            return await contactsComponent.UpdateContactStatus(ContactId);
        }
    }
}

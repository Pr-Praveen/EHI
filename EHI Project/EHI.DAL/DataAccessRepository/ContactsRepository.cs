using EHI.DAL.DataModel;
using EHI.DAL.IDataAccessRepository;
using EHI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHI.DAL.DataAccessRepository
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly EHIContext _dbContext = null;
        public ContactsRepository(EHIContext EHIContext)
        {
            _dbContext = EHIContext;
        }


        public async Task<IEnumerable<ContactViewModel>> GetAllContacts()
        {
            var result = await (from x in _dbContext.Contacts
                               select new ContactViewModel
                               {
                                   Id = x.Id,
                                   FirstName = x.FirstName,
                                   LastName = x.LastName,
                                   Email = x.Email,
                                   Phone = x.Phone,
                                   IsActive = x.IsActive,
                                   CreatedDate = x.CreatedDate,
                                   CreatedById = x.CreatedById

                               }).ToListAsync();

            return result;
        }

        public async Task<ContactViewModel> GetContact(Guid ContactId)
        {
            var result = await (from x in _dbContext.Contacts
                                where x.Id.Equals(ContactId)
                                select new ContactViewModel
                                {
                                    Id = x.Id,
                                    FirstName = x.FirstName,
                                    LastName = x.LastName,
                                    Email = x.Email,
                                    Phone = x.Phone,
                                    IsActive = x.IsActive,
                                    CreatedDate = x.CreatedDate,
                                    CreatedById = x.CreatedById

                                }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> AddContact(ContactViewModel model)
        {
            bool isSuccess = false;
            Contacts contacts = new Contacts
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                IsActive = model.IsActive
            };

            try
            {
                _dbContext.Contacts.Add(contacts);
                await _dbContext.SaveChangesAsync();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public async Task<bool> UpdateContact(ContactViewModel model)
        {
            bool isSuccess = false;
          
            try
            {
                var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));
                if (contact != null)
                {
                    contact.FirstName = model.FirstName;
                    contact.LastName = model.LastName;
                    contact.Email = model.Email;
                    contact.Phone = model.Phone;
                    contact.IsActive = model.IsActive;
                    //contacts.ModifiedDate = DateTime.Now();
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public async Task<bool> UpdateContactStatus(Guid ContactId)
        {
            bool isSuccess = false;
          
            try
            {
                var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id.Equals(ContactId));
                if (contact != null)
                {
                    contact.IsActive = !contact.IsActive;
                    //contacts.ModifiedDate = DateTime.Now();
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}

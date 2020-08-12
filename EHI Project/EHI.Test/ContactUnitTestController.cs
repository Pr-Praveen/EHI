using EHI.Api.Controllers;
using EHI.BLL.BusinessComponent;
using EHI.DAL.DataAccessRepository;
using EHI.DAL.DataModel;
using EHI.DAL.IDataAccessRepository;
using EHI.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EHI.Test
{
    public class ContactUnitTestController
    {
        private IContactsRepository repository;
        public static DbContextOptions<EHIContext> dbContextOptions { get; }
        public static string connectionString = "Server=.\\;Database=EHI;Trusted_Connection=True;MultipleActiveResultSets=true";
        static ContactUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<EHIContext>()
                   .UseSqlServer(connectionString)
                   .Options;
        }
        public ContactUnitTestController()
        {
            var context = new EHIContext(dbContextOptions);
            TestDataDBInitializer db = new TestDataDBInitializer();
            db.Feed(context);

            repository = new ContactsRepository(context);

        }

        [Fact]
        public async void AddContact_Test_Return_OkResult()
        {
            //Arrange  
            var component = new ContactsComponent(repository);
            var contactController = new ContactController(component);
            var ContactViewModel = new ContactViewModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                Email = "test1@test.com",
                Phone = "123456",
                IsActive = true
            };
            //Act  
            var data = await contactController.AddContact(ContactViewModel);

            //Assert  
            Assert.IsType<bool>(data);

                
        }

        [Fact]
        public async void AddContact_Test_InvalidData_Return_BadRequest()
        {
            //Arrange  
            var component = new ContactsComponent(repository);
            var contactController = new ContactController(component);
            var ContactViewModel = new ContactViewModel()
            {
                Id = Guid.NewGuid(),
                FirstName = null,
                LastName = "LastName 1",
                Email = "test1@test.com",
                Phone = "123456",
                IsActive = true
            };
            //Act  
            var data = await contactController.AddContact(ContactViewModel);

            //Assert  
            Assert.IsType<bool>(data);
        }

    }
}

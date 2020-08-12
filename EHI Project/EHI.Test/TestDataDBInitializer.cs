using EHI.DAL.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EHI.Test
{
    public class TestDataDBInitializer
    {
        public TestDataDBInitializer()
        {
        }

        public void Feed(EHIContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Contacts.AddRange(
                new Contacts() {
                    Id = Guid.NewGuid(),
                    FirstName = "FirstName 1",
                    LastName = "LastName 1",
                    Email = "test1@test.com",
                    Phone = "123456",
                    IsActive = true
                },
                new Contacts()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "FirstName 2",
                    LastName = "LastName 2",
                    Email = "test2@test.com",
                    Phone = "123456",
                    IsActive = true
                },
                 new Contacts()
                 {
                     Id = Guid.NewGuid(),
                     FirstName = "FirstName 3",
                     LastName = "LastName 3",
                     Email = "test3@test.com",
                     Phone = "123456",
                     IsActive = true
                 }
            );

         
            context.SaveChanges();
        }
    }
}

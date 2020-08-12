using System;
using System.Collections.Generic;

namespace EHI.DAL.DataModel
{
    public partial class Contacts
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedById { get; set; }
    }
}

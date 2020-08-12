using System;
using System.Collections.Generic;

namespace EHI.DAL.DataModel
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public byte? UserTypeId { get; set; }
        public string PassWord { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UserCreatedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? UserModifiedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? UserDeletedById { get; set; }
        public bool IsUserLocked { get; set; }
    }
}

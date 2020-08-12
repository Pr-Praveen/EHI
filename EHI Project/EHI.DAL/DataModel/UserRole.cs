using System;
using System.Collections.Generic;

namespace EHI.DAL.DataModel
{
    public partial class UserRole
    {
        public Guid Id { get; set; }
        public string UserRoleName { get; set; }
        public byte UserType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UserCreatedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? UserModifiedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? UserDeletedById { get; set; }
    }
}

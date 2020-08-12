using System;
using System.Collections.Generic;

namespace EHI.DAL.DataModel
{
    public partial class Website
    {
        public Guid Id { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteUrl { get; set; }
        public Guid WebsiteTypeId { get; set; }
        public int MaxLimit { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? WebsiteCategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UserCreatedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? UserModifiedById { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Guid? UserDeletedById { get; set; }
    }
}

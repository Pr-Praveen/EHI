using System;
using System.Collections.Generic;
using System.Text;

namespace EHI.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}

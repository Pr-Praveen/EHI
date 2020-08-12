using System;
using System.Collections.Generic;
using System.Text;

namespace EHI.Models
{
    public class ViewData : IViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

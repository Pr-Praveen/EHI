using System;
using System.Collections.Generic;
using System.Text;

namespace EHI.Models
{
    public interface IViewModel
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}

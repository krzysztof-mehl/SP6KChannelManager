using System;
using System.Collections.Generic;
using System.Text;

namespace SP6KChannelManager.Models
{
    public class ErrorHandler
    {
        public bool HasError { get; set; } = false;

        public List<string> ErrorMessages { get; set; } = [];
    }
}

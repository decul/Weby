using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weby.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActivated { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSuperAdmin { get; set; }

    }
}
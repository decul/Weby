using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weby
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Super = "SuperAdmin";
        public const string Active = "Activated";


        public static string[] All()
        {
            return new string[]
            {
                Role.Active,
                Role.Admin,
                Role.Super
            };
        }
    }
}
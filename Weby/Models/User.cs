using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Weby.Models
{
    public class User
    {
        public string Id { get; set; }

        [Column("UserName")]
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }



        public ICollection<Reservation> Reservations { get; set; }
    }
}
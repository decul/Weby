using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weby.Models
{
    public class Day
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }


        public ICollection<Reservation> Reservations { get; set; }
    }
}
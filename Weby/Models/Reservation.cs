using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weby.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime SubmissionDate { get; set; }

        public bool OnBackupList { get; set; }

        public bool Confirmed { get; set; }



        public User User { get; set; }

        public ICollection<Day> Days { get; set; }
    }
}

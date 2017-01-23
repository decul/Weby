using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Weby.Models.ViewModels
{
    public class NewReservationViewModels
    {
        public bool OnBackupList { get; set; }

        public bool Confirmed { get; set; }



        [Required]
        public string Dates { get; set; }
    }
}
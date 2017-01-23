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


        public virtual ICollection<Reservation> Reservations { get; set; }


        static public List<string> BusyDaysStringList(string userId)
        {
            var db = new ApplicationDbContext();
            var days = db.Days.Where(d => d.Reservations.Where(r => r.User.Id != userId).Any());
            var list = new List<string>();
            foreach (var day in days)
                list.Add(day.Date.Day.ToString("D2") + "/" + day.Date.Month.ToString("D2") + "/" + day.Date.Year);
            db.Dispose();
            return list;
        }

        static public List<string> UsersDaysStringList(string userId, int reservationId = -1)
        {
            var db = new ApplicationDbContext();
            List<Day> days;
            if (reservationId > -1)
                days = db.Days.Where(d => d.Reservations.Where(r => r.User.Id == userId).Any()).ToList();
            else
                days = db.Days.Where(d => d.Reservations.Where(r => r.User.Id == userId && r.Id != reservationId).Any()).ToList();
            var list = new List<string>();
            foreach (var day in days)
                list.Add(day.Date.Day.ToString("D2") + "/" + day.Date.Month.ToString("D2") + "/" + day.Date.Year);
            db.Dispose();
            return list;
        }
    }
}
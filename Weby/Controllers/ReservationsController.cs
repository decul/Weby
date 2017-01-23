using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Weby.Models;
using Weby.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace Weby.Controllers
{
    [Authorize(Roles = Role.Active)]
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.BusyDays = Day.BusyDaysStringList(userId);
            ViewBag.UsersDays = Day.UsersDaysStringList(userId);
            var usersRes = db.Reservations
                .Where(r => r.User.Id == userId)
                .OrderBy(r => r.Days.FirstOrDefault().Date).ToList();
            return View(usersRes);
        }

        public ActionResult UsersReservations()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Reservations.Where(r => r.User.Id == userId).ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.BusyDays = Day.BusyDaysStringList(User.Identity.GetUserId());
            ViewBag.UsersDays = Day.UsersDaysStringList(User.Identity.GetUserId());
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OnBackupList,Confirmed,Dates")] NewReservationViewModels newReservation)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation()
                {
                    Confirmed = newReservation.Confirmed,
                    OnBackupList = newReservation.OnBackupList,
                    SubmissionDate = DateTime.Now,
                    User = db.Users.Find(User.Identity.GetUserId()),
                    Days = new List<Day>()
                };

                string[] dates = newReservation.Dates.Split(',');
                var startDate = DateTime.ParseExact(dates.First(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(dates.Last(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (endDate < startDate)
                {
                    var temp = startDate;
                    startDate = endDate;
                    endDate = temp;
                }
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    Day day = db.Days.SingleOrDefault(d => d.Date == date);
                    if (day != null)
                        reservation.Days.Add(day);
                    else
                        reservation.Days.Add(new Day() { Date = date });
                }

                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusyDays = Day.BusyDaysStringList(User.Identity.GetUserId());
            ViewBag.UsersDays = Day.UsersDaysStringList(User.Identity.GetUserId());
            return View(newReservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            ViewBag.BusyDays = Day.BusyDaysStringList(userId);
            ViewBag.UsersDays = Day.UsersDaysStringList(userId, id.Value);
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View("Create", reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubmissionDate,OnBackupList,Confirmed")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

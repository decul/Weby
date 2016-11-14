using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weby.Models;

namespace Weby.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        WebyDb db = new WebyDb();
        ApplicationDbContext _db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            var model = new List<UserViewModel>();

            var users = db.Users;

            foreach (var user in users)
            {
                _db.Users.Find(user)
            }

            var userModel = new UserViewModel() { }
            return View();
        }
    }
}
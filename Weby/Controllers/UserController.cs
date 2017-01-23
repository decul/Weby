using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Weby.Models;

namespace Weby.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class UserController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            var users = context.Users.ToList();

            var list = new List<UserViewModel>();
            
            foreach (var user in users)
            {
                var model = new UserViewModel();
                model.Id = user.Id;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.IsActivated = userManager.IsInRole(user.Id, Role.Active);
                model.IsAdmin = userManager.IsInRole(user.Id, Role.Admin);
                list.Add(model);
            }
            
            return View(list);
        }

        //[ValidateAntiForgeryToken]
        public ActionResult Activate(UserViewModel model)
        {
            if (!userManager.IsInRole(model.Id, Role.Active))
                userManager.AddToRole(model.Id, Role.Active);
            return RedirectToAction("Index");
        }

        //[ValidateAntiForgeryToken]
        public ActionResult Deactivate(UserViewModel model)
        {
            if (!userManager.IsInRole(model.Id, Role.Super) && model.Id != User.Identity.GetUserId())
            {
                if (userManager.IsInRole(model.Id, Role.Active))
                    userManager.RemoveFromRole(model.Id, Role.Active);
                if (userManager.IsInRole(model.Id, Role.Admin))
                    userManager.RemoveFromRole(model.Id, Role.Admin);
            }
            return RedirectToAction("Index");
        }

        //[ValidateAntiForgeryToken]
        public ActionResult MakeAdmin(UserViewModel model)
        {
            if (!userManager.IsInRole(model.Id, Role.Admin))
                userManager.AddToRole(model.Id, Role.Admin);
            return RedirectToAction("Index");
        }

        //[ValidateAntiForgeryToken]
        public ActionResult RemoveAdmin(UserViewModel model)
        {
            if (userManager.IsInRole(model.Id, Role.Admin) && !userManager.IsInRole(model.Id, Role.Super) && model.Id != User.Identity.GetUserId())
                userManager.RemoveFromRole(model.Id, Role.Admin);
            return RedirectToAction("Index");
        }

    }
}
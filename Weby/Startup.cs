using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Weby.Models;

[assembly: OwinStartupAttribute(typeof(Weby.Startup))]
namespace Weby
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }


        // Create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

    
            if (!roleManager.RoleExists("Activated"))
            {
                var role = new IdentityRole();
                role.Name = "Activated";
                roleManager.Create(role);
            }
   
            if (!roleManager.RoleExists("Admin"))
            { 
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
   
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                // Create SuperAdmin role 
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                // Create the SuperAdmin   
                var user = new ApplicationUser();
                user.UserName = "Decul";
                user.Email = "11tomdec@gmail.com";
                string userPWD = "Pa$$w0rd";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "SuperAdmin");
                    UserManager.AddToRole(user.Id, "Admin");
                    UserManager.AddToRole(user.Id, "Activated");
                }
            }
        }
    }
}

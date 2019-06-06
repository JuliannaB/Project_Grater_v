using Grater.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using System.Web.UI.WebControls;

[assembly: OwinStartupAttribute(typeof(Grater.Startup))]
namespace Grater
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var roleManager = new RoleManager<CustomRole, int>(new RoleStore<CustomRole, int, CustomUserRole>(_context));
            var UserManager = new UserManager<ApplicationUser, int>(new UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(_context));


            // In Startup I am creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new CustomRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "Bombel@gmail.com";
                user.Email = "Bombel@gmail.com";

                string userPWD = "Dupa12345!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
            // creating Creating Manager role    
            if (!roleManager.RoleExists("Therapist"))
            {
                var role = new CustomRole();
                role.Name = "Therapist";
                roleManager.Create(role);

              /*  var user = new ApplicationUser();
                user.UserName = "Therapistka@gmail.com";
                user.Email = "Therapistka@gmail.com";

                string userPWD = "Dupa12345!";

                var tUser = UserManager.Create(user, userPWD);

                if (tUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Therapist");
                }  */

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("User"))
            {
                var role = new CustomRole();
                role.Name = "User";
                roleManager.Create(role);

            }
        }
    }


}


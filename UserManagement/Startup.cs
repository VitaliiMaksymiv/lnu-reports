using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using UserManagement.Models;

[assembly: OwinStartup(typeof(UserManagement.Startup))]
namespace UserManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Ректор"))
            {

                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Ректор";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "marycholeg@gmail.com";
                user.Email = "marycholeg@gmail.com";
                user.FirstName = "marycholeg@gmail.com";
                user.LastName = "marycholeg@gmail.com";
                user.FathersName = "marycholeg@gmail.com";
                user.BirthDate = DateTime.Now;
                user.DefenseYear = DateTime.Now;
                user.AwardingDate = DateTime.Now;
                user.GraduationDate = DateTime.Now;
                string userPWD = "qwerty1";

                var chkUser = UserManager.Create(user, userPWD);
 
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Ректор");

                }
            }
            
            if (!roleManager.RoleExists("Викладач"))
            {
                var role = new IdentityRole();
                role.Name = "Викладач";
                roleManager.Create(role);

            }
 
            if (!roleManager.RoleExists("Декан"))
            {
                var role = new IdentityRole();
                role.Name = "Декан";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Завідувач кафедри"))
            {
                var role = new IdentityRole();
                role.Name = "Завідувач кафедри";
                roleManager.Create(role);
            }
        }
    }
}

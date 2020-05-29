using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Data.Entity;
using UserManagement.Migrations;
using UserManagement.Models;
using UserManagement.Models.db;

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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Superadmin"))
            {
                // first we create Admin rool   
                var role = new IdentityRole();
                role.Name = "Superadmin";
                roleManager.Create(role); 

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "superadmin@lnu.edu.ua";
                user.Email = "superadmin@lnu.edu.ua";
                user.BirthDate = DateTime.Now;
                user.DefenseYear = DateTime.Now;
                user.AwardingDate = DateTime.Now;
                user.GraduationDate = DateTime.Now;
                user.IsActive = true;
                string userPWD = "qwerty1";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Superadmin");

                }
            }

            if (!roleManager.RoleExists("Викладач"))
            {
                var role = new IdentityRole();
                role.Name = "Викладач";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Адміністрація ректорату"))
            {
                var role = new IdentityRole();
                role.Name = "Адміністрація ректорату";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Адміністрація деканату"))
            {
                var role = new IdentityRole();
                role.Name = "Адміністрація деканату";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Керівник кафедри"))
            {
                var role = new IdentityRole();
                role.Name = "Керівник кафедри";
                roleManager.Create(role);
            }

            
        }
    }
}

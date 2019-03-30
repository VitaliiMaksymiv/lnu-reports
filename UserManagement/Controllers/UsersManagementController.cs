using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersManagementController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: UsersManagement
        public ActionResult Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            List<ApplicationUser> list = db.Users.ToList();
            Dictionary <string, List<string>> map = new Dictionary<string, List<string>>();
            list.ForEach(x =>
            {
                map.Add(x.Id, x.Roles.Select(y => db.Roles.Find(y.RoleId).Name).ToList());
            });
            ViewBag.RolesForThisUser = map;
            return View(db.Users.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: UsersManagement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var userRoles = applicationUser.Roles.Select(y => db.Roles.Find(y.RoleId).Name).ToList();
            ViewBag.RolesForThisUser = userRoles;
            return View(applicationUser);
        }

        // GET: UsersManagement/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var userRoles = applicationUser.Roles.Select(y => db.Roles.Find(y.RoleId).Name).ToList();
            ViewBag.RolesForThisUser = userRoles;
            ViewBag.AllRoles = db.Roles.Where(x => !userRoles.Contains(x.Name)).Select(x => x.Name);
            return View(applicationUser);
        }

        // POST: UsersManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,LastName,FathersName,IsActive,PasswordHash,SecurityStamp")] ApplicationUser applicationUser, [Bind(Include = "RoleToAdd")] string roleToAdd )
        {
            var userRoles = applicationUser.Roles.Select(y => db.Roles.Find(y.RoleId).Name).ToList();
            ViewBag.RolesForThisUser = userRoles;
            ViewBag.AllRoles = db.Roles.Where(x => !userRoles.Contains(x.Name)).Select(x => x.Name);
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(applicationUser.Id);
                if (applicationUser.IsActive && (roleToAdd == null || roleToAdd.Equals("")) && user.Roles.Count == 0)
                {
                    ViewBag.Error = "Impossible to make active user without role";
                    return View(applicationUser);
                }
                applicationUser.UserName = applicationUser.Email;
                if (roleToAdd != null && !roleToAdd.Equals("") && !user
                    .Roles.Any(x=> db.Roles.Find(x.RoleId).Equals(roleToAdd)))
                {
                    UserManager.AddToRole(applicationUser.Id, roleToAdd);
                }
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        [HttpGet]
        public ActionResult DeleteRole(String userId, String roleName)
        {
            UserManager.RemoveFromRole(userId, roleName);
            return RedirectToAction("Edit", "UsersManagement", new { id = userId });
        }
    }
}

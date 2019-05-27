using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;
using UserManagement.Models.db;
using UserManagement.Models.PublicationDB;

namespace UserManagement.Controllers
{
    [Authorize]
    public class PublicationsController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        
        private UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        // GET: Publications
        public ActionResult Index(int? page, bool? isMine, bool? isFaculty, string searchString)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            bool isMineWihoutNull = isMine ?? false;
            bool isFacultyWihoutNull = isFaculty ?? false;
            var isDupe = UserManager.IsInRole(User.Identity.GetUserId(), "Декан");
            ViewBag.isMine = isMineWihoutNull;
            ViewBag.isFaculty = isFacultyWihoutNull;
            ViewBag.page = pageNumber;
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(db.Publication
                    .Where(s => s.Name.Contains(searchString))
                    .ToList()
                    .ToPagedList(pageNumber, pageSize));
            }
            if (isMineWihoutNull)
            {
                if (isDupe && isFacultyWihoutNull)
                {
                    var dupe = UserManager.FindByName(User.Identity.Name);
                    return View(db.Publication
                    .Where(x => x.User.Any(y => y.UserName == User.Identity.Name))
                    .Where(x => x.User.Any(y => y.Cathedra.Faculty.ID == dupe.Cathedra.Faculty.ID))
                    .ToList()
                    .ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return View(db.Publication
                    .Where(x => x.User.Any(y => y.UserName == User.Identity.Name))
                    .ToList()
                    .ToPagedList(pageNumber, pageSize));
                }
            }
            else
            {
                if (isDupe && isFacultyWihoutNull)
                {
                    var dupe = UserManager.FindByName(User.Identity.Name);
                    return View(db.Publication
                    .Where(x => x.User.Any(y => y.Cathedra.Faculty.ID == dupe.Cathedra.Faculty.ID))
                    .ToList()
                    .ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    return View(db.Publication
                    .ToList()
                    .ToPagedList(pageNumber, pageSize));
                }
            }
        }

        // GET: Publications/Details/5
        public ActionResult Details(int? id)
        {
            var isAdmin = UserManager.IsInRole(User.Identity.GetUserId(), "Ректор");
            if (!isAdmin)
            {
                if (UserManager.FindById(User.Identity.GetUserId()).Publication.Where(x => x.ID == id).FirstOrDefault() == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publication.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublicationUsers = String.Join(", ", publication.User
                .Select(x =>String.Join(" ", x.LastName, x.FirstName, x.FathersName))
                    .ToList());
            return View(publication);
        }

        // GET: Publications/Create
        public ActionResult Create()
        {
            var users = db.Users.Where(x => x.IsActive == true).ToList();
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            ViewBag.AllUsers = users
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.LastName, x.FirstName, x.FathersName),
                         Value = x.Id
                     })
                    .ToList();
            return View();
        }

        // POST: Publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,OtherAuthors,Date,SizeOfPages,PublicationType")] Publication publication,
            [Bind(Include = "UserToAdd")]String[] userToAdd)
        {
            var users = db.Users.ToList();
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            ViewBag.AllUsers = users
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.LastName, x.FirstName, x.FathersName),
                         Value = x.Id
                     })
                    .ToList();
            if (ModelState.IsValid)
            {
                if (userToAdd != null && userToAdd.Length != 0)
                {
                    foreach (var current in userToAdd)
                    {
                        var user = db.Users.Find(current);
                        publication.User.Add(user);
                        user.Publication.Add(publication);
                    }
                }
                db.Publication.Add(publication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publication);
        }

        // GET: Publications/Edit/5
        [Authorize(Roles = "Ректор, Декан")]
        public ActionResult Edit(int? id)
        {
            var isAdmin = UserManager.IsInRole(User.Identity.GetUserId(), "Ректор");
            if (!isAdmin)
            {
                if (UserManager.FindById(User.Identity.GetUserId()).Publication.Where(x => x.ID == id).FirstOrDefault() == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publication.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem {Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var users = db.Users.ToList();
            ViewBag.AllUsers = users
                .Where(y => !publication.User.Contains(y) && y.IsActive)
                .Select(x =>
                     new SelectListItem
                    {
                        Selected = false,
                        Text = String.Join(" ", x.LastName, x.FirstName, x.FathersName),
                        Value = x.Id
                    })
                    .ToList();
            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,OtherAuthors,Date,SizeOfPages,PublicationType")] Publication publication,
            [Bind(Include = "UserToAdd")]String userToAdd)
        {
            var isAdmin = UserManager.IsInRole(User.Identity.GetUserId(), "Ректор");
            if (!isAdmin)
            {
                if (UserManager.FindById(User.Identity.GetUserId()).Publication.Where(x => x.ID == publication.ID).FirstOrDefault() == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            if (ModelState.IsValid)
            {
                var publicationFromDB = db.Publication.Find(publication.ID);
                publicationFromDB.Name = publication.Name;
                publicationFromDB.OtherAuthors = publication.OtherAuthors;
                publicationFromDB.PublicationType = publication.PublicationType;
                publicationFromDB.SizeOfPages = publication.SizeOfPages;
                publicationFromDB.Date = publication.Date;
                if (userToAdd != null && userToAdd != "")
                {
                    var publicationFormDb = db.Publication.Find(publication.ID);
                    var user = db.Users.Find(userToAdd);
                    publicationFormDb.User.Add(user);
                    user.Publication.Add(publicationFormDb);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            var users = db.Users.ToList();
            ViewBag.AllUsers = users
                .Where(y => !publication.User.Contains(y))
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.LastName, x.FirstName, x.FathersName),
                         Value = x.Id
                     })
                    .ToList();
            return View(publication);
        }

        // GET: Publications/Delete/5
        [Authorize(Roles = "Ректор")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publication.Find(id);
            if (publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Ректор")]
        public ActionResult DeleteConfirmed(int id)
        {
            Publication publication = db.Publication.Find(id);
            db.Publication.Remove(publication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteUser(String userId, int publicationId)
        {
            var publication = db.Publication.Find(publicationId);
            var user = publication.User.Where(x => x.Id == userId).FirstOrDefault();
            if (user != null)
            {
                publication.User.Remove(user);
                user.Publication.Remove(publication);
                db.SaveChanges();
            }
            return RedirectToAction("Edit", "Publications", new { id = publicationId });
        }

    }
}

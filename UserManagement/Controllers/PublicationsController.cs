﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
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
        //public static ApplicationDbContext db
        //{
        //    get
        //    {
        //        return DB ?? new ApplicationDbContext();
        //    }
        //    private set
        //    {
        //        DB = value;
        //    }
        //}

        private UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        // GET: Publications
        public ActionResult Index(int? page, bool? isMine, string searchString, string dateFrom, string dateTo, int? cathedra, int? faculty)
        {
            db = new ApplicationDbContext();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            bool isMineWihoutNull = isMine ?? true;
            int cathedraNumber = cathedra ?? -1;
            int facultyNumber = faculty ?? -1;
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            ViewBag.isMine = isMineWihoutNull;
            ViewBag.cathedra = cathedraNumber;
            ViewBag.faculty = facultyNumber;
            ViewBag.searchString = searchString;
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.page = pageNumber;
            PutCathedraAndFacultyIntoViewBag();
            var allPublications = db.Publication
                .Where(x => cathedraNumber == -1 || (cathedraNumber != -1 && x.User.Any(y => y.Cathedra.ID == cathedraNumber)))
                .Where(x => facultyNumber == -1 || (facultyNumber != -1 && x.User.Any(y => y.Cathedra.Faculty.ID == facultyNumber)))
                .ToList();
            allPublications = allPublications
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && Convert.ToDateTime(x.Date) >= DateTime.Parse(dateFromVerified)))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && Convert.ToDateTime(x.Date) <= DateTime.Parse(dateToVerified)))
                .ToList();
            return GetRightPublicationView(allPublications, isMineWihoutNull, pageNumber, pageSize, searchString);
        }

        private void PutCathedraAndFacultyIntoViewBag()
        {
            var cathedas = db.Cathedra.ToList();
            var faculties = db.Faculty.ToList();
            ViewBag.AllCathedras = cathedas
                .Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.ID.ToString()
                     }).ToList();
            ViewBag.AllFaculties = faculties
                .Select(x =>
                     new SelectListItem
                     {
                         Text = x.Name,
                         Value = x.ID.ToString()
                     }).ToList();
        }

        private ActionResult GetRightPublicationView(List<Publication> allPublications, bool isMineWihoutNull,
                                                        int pageNumber, int pageSize, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(allPublications
                    .Where(s => s.Name.Contains(searchString))
                    .ToList()
                    .ToPagedList(pageNumber, pageSize));
            }
            if (isMineWihoutNull)
            {
                return View(allPublications
                .Where(x => x.User.Any(y => y.UserName == User.Identity.Name))
                .ToList()
                .ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(allPublications
                .ToList()
                .ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Publications/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.PublicationUsers = String.Join(", ", publication.User
                .Select(x => String.Join(" ", x.I18nUserInitials.Single(y => y.Language == publication.Language).LastName,
                                              x.I18nUserInitials.Single(y => y.Language == publication.Language).FirstName,
                                              x.I18nUserInitials.Single(y => y.Language == publication.Language).FathersName))
                    .ToList());
            return View(publication);
        }

        // GET: Publications/Create
        public ActionResult Create(string language)
        {
            var languageVerified = language == null || language == "" ? "UA" : language;
            var users = db.Users.Where(x => x.IsActive == true).ToList();
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            ViewBag.AllUsers = users
                .Where(x => UserManager.IsInRole(x.Id, "Викладач") || UserManager.IsInRole(x.Id, "Керівник кафедри")
                || UserManager.IsInRole(x.Id, "Адміністрація ректорату") || UserManager.IsInRole(x.Id, "Адміністрація деканату"))
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.Single(y => y.Language == (Language)Enum.Parse(typeof(Language), languageVerified)).LastName,
                                                 x.I18nUserInitials.Single(y => y.Language == (Language)Enum.Parse(typeof(Language), languageVerified)).FirstName,
                                                 x.I18nUserInitials.Single(y => y.Language == (Language)Enum.Parse(typeof(Language), languageVerified)).FathersName),
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
        public ActionResult Create([Bind(Include = "ID,Name,OtherAuthors,Date,SizeOfPages,PublicationType,Place," +
            "MainAuthor,IsMainAuthorRegistered,Language,Link,Edition,Magazine,DOI,Tome")] Publication publication,
            [Bind(Include = "UserToAdd")]String[] userToAdd)
        {
            var users = db.Users.Where(x => x.Roles.Count == 1 && x.Roles.Any(y => y.RoleId != db.Roles.Where(z => z.Name == "Superadmin").FirstOrDefault().Id)).ToList();
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            ViewBag.AllUsers = users
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).LastName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FirstName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FathersName),
                         Value = x.Id
                     })
                    .ToList();
            if (ModelState.IsValid)
            {
                publication.User.Add(db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault());
                if (userToAdd != null)
                {
                    var publicationExists = db.Publication
                        .Any(x =>
                        x.Name == publication.Name
                        && userToAdd.All(y => x.User.Select(z => z.Id).Contains(y))
                        && x.PublicationType == publication.PublicationType
                        );
                    if (publicationExists)
                    {
                        ModelState.AddModelError("", "Така публікація вже існує");
                        return View(publication);
                    }
                    if (userToAdd.Length != 0)
                    {
                        foreach (var current in userToAdd)
                        {
                            var user = db.Users.Find(current);
                            publication.User.Add(user);
                            user.Publication.Add(publication);
                        }
                    }
                }
                publication.MainAuthor = User.Identity.Name;
                db.Publication.Add(publication);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publication);
        }

        // GET: Publications/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var users = db.Users.ToList();
            ViewBag.AllUsers = users
                .Where(x => UserManager.IsInRole(x.Id, "Викладач") || UserManager.IsInRole(x.Id, "Адміністрація ректорату") ||
                UserManager.IsInRole(x.Id, "Адміністрація деканату") || UserManager.IsInRole(x.Id, "Керівник кафедри"))
                .Where(y => !publication.User.Contains(y) && y.IsActive)
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).LastName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FirstName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FathersName),
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
        public ActionResult Edit([Bind(Include = "ID,Name,OtherAuthors,Date,SizeOfPages,PublicationType,Language," +
            "Link,Edition,Magazine,DOI,Tome")] Publication publication,
            [Bind(Include = "UserToAdd")]String[] userToAdd)
        {
            ViewBag.AllPublicationTypes = Enum.GetNames(typeof(PublicationType))
                .Select(x => new SelectListItem { Selected = false, Text = x, Value = x }).ToList();
            ViewBag.AllLanguages = Enum.GetNames(typeof(Language))
                .Select(x => new SelectListItem { Selected = false, Text = x.Replace('_', ' '), Value = x }).ToList();
            var users = db.Users.Where(x => x.Roles.Count == 1 && x.Roles.Any(y => y.RoleId != db.Roles.Where(z => z.Name == "Superadmin").FirstOrDefault().Id)).ToList();
            ViewBag.AllUsers = users
                .Where(y => !publication.User.Contains(y))
                .Select(x =>
                     new SelectListItem
                     {
                         Selected = false,
                         Text = String.Join(" ", x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).LastName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FirstName,
                                                 x.I18nUserInitials.FirstOrDefault(y => y.Language == publication.Language).FathersName),
                         Value = x.Id
                     })
                    .ToList();
            if (ModelState.IsValid && userToAdd != null)
            {
                var publicationExists = db.Publication
                    .Any(x =>
                    x.ID != publication.ID
                    && x.Name == publication.Name
                    && userToAdd.All(y => x.User.Select(z => z.Id).Contains(y))
                    && x.PublicationType == publication.PublicationType
                    );
                if (publicationExists)
                {
                    ModelState.AddModelError("", "Така публікація вже існує");
                    return View(publication);
                }
                var publicationFromDB = db.Publication.Find(publication.ID);
                publicationFromDB.Name = publication.Name;
                publicationFromDB.OtherAuthors = publication.OtherAuthors;
                publicationFromDB.PublicationType = publication.PublicationType;
                publicationFromDB.SizeOfPages = publication.SizeOfPages;
                publicationFromDB.Language = publication.Language;
                publicationFromDB.Date = publication.Date;
                if (userToAdd != null && userToAdd.Length != 0)
                {
                    foreach (var current in userToAdd)
                    {
                        var publicationFormDb = db.Publication.Find(publication.ID);
                        var user = db.Users.Find(current);
                        publicationFormDb.User.Add(user);
                        user.Publication.Add(publicationFormDb);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publication);
        }

        // GET: Publications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication publication = db.Publication.Find(id);
            if (publication.AcceptedToPrintPublicationReport.Union(publication.PrintedPublicationReport.Union(publication.RecomendedPublicationReport)).Count() > 0)
            {
                ViewBag.Exists = "Ця публікація включена в звіт. Неможливо видалити.";
            }
            if (publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publication publication = db.Publication.Find(id);
            if (publication.AcceptedToPrintPublicationReport.Union(publication.PrintedPublicationReport.Union(publication.RecomendedPublicationReport)).Count() > 0)
            {
                return RedirectToAction("Index");
            }
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
        protected override void Dispose(bool disposing)
        {
            //if (disposing && DB != null)
            //{
            //    DB.Dispose();
            //    DB = null;
            //}

            //base.Dispose(disposing);
        }
    }
}

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
using UserManagement.Models.db;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри")]
    public class ThemeOfScientificWorksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ThemeOfScientificWorks
        public ActionResult Index(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var user = db.Users.Where(x => x.UserName == User.Identity.Name).First();
            var scientifthemes = db.ThemeOfScientificWork.Where(x => x.Cathedra.ID == user.Cathedra.ID).ToList();
            return View(scientifthemes.ToPagedList(pageNumber, pageSize));
        }

        // GET: ThemeOfScientificWorks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Create
        public ActionResult Create()
        {
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Selected = false, Text = x.ToLower().Replace('_', ' '), Value = x }).ToList();
            return View();
        }

        // POST: ThemeOfScientificWorks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Value,ScientificHead,PeriodFrom,PeriodTo,Financial,ThemeNumber")] ThemeOfScientificWork themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.UserName == User.Identity.Name).First();
                themeOfScientificWork.Cathedra = db.Cathedra.Where(x => x.ID == user.Cathedra.ID).First();
                db.ThemeOfScientificWork.Add(themeOfScientificWork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            ViewBag.AllFinancials = Enum.GetNames(typeof(Financial))
                .Select(x => new SelectListItem { Selected = false, Text = x.ToLower(), Value = x }).ToList();
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // POST: ThemeOfScientificWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Value,ScientificHead,PeriodFrom,PeriodTo,ThemeNumber,Financial")] ThemeOfScientificWork themeOfScientificWork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(themeOfScientificWork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(themeOfScientificWork);
        }

        // GET: ThemeOfScientificWorks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            if (themeOfScientificWork == null)
            {
                return HttpNotFound();
            }
            return View(themeOfScientificWork);
        }

        // POST: ThemeOfScientificWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThemeOfScientificWork themeOfScientificWork = db.ThemeOfScientificWork.Find(id);
            db.ThemeOfScientificWork.Remove(themeOfScientificWork);
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

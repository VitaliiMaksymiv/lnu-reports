using Microsoft.AspNet.Identity;
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
using UserManagement.Models.Reports;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри")]
    public class ReportCathedraListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportList
        public ActionResult Index(int? page, string dateFrom, string dateTo)
        {
            db = new ApplicationDbContext();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.page = pageNumber;
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            List<CathedraReport> reports;
            var parsedDateFrom = dateFromVerified != "" ? DateTime.Parse(dateFromVerified) : DateTime.Now;
            var parsedDateTo = dateToVerified != "" ? DateTime.Parse(dateToVerified) : DateTime.Now;
            reports = db.CathedraReport.Where(x => x.User.Cathedra.ID == currentUser.Cathedra.ID)
            .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
            .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
            .ToList();
            return View(reports.ToPagedList(pageNumber, pageSize));
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

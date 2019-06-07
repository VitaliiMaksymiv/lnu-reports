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

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Викладач, Керівник кафедри")]
    public class ReportListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportList
        public ActionResult Index(int? page, string dateFrom, string dateTo)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.page = pageNumber;
            var currentUser = db.Users.Find(User.Identity.GetUserId());
            List<Report> reports;
            var parsedDateFrom = dateFromVerified != "" ? DateTime.Parse(dateFromVerified) : DateTime.Now;
            var parsedDateTo = dateToVerified != "" ? DateTime.Parse(dateToVerified) : DateTime.Now;
            if (User.IsInRole("Керівник кафедри"))
            {
                reports = db.Reports.Where(x => (x.User.Cathedra.ID == currentUser.Cathedra.ID)
                && (x.User.Id == currentUser.Id || (x.User.Id != currentUser.Id && x.IsSigned)))
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .ToList();
            }
            else
            {
                reports = db.Reports.Where(x => x.User.Id == currentUser.Id)
                .Where(x => dateFromVerified == "" || (dateFromVerified != "" && x.Date.Value >= parsedDateFrom))
                .Where(x => dateToVerified == "" || (dateToVerified != "" && x.Date.Value <= parsedDateTo))
                .ToList();
            }
            return View(reports.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Sign(int reportId)
        {
            var report = db.Reports.Find(reportId);
            report.IsSigned = true;
            db.SaveChanges();
            return RedirectToAction("Index", "ReportList");
        }
        public ActionResult Confirm(int reportId)
        {
            var report = db.Reports.Find(reportId);
            report.IsConfirmed = true;
            db.SaveChanges();
            return RedirectToAction("Index", "ReportList");
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

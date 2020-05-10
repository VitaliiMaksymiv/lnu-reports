using Microsoft.AspNet.Identity;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using UserManagement.Converter;
using UserManagement.Models;
using UserManagement.Models.db;
using UserManagement.Models.Reports;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    public class ReportsController : Controller
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private ReportService reportService = new ReportService(db);
        // GET: Report
        public ActionResult Index(string dateFrom, string dateTo, int? stepIndex, int? reportId)
        {
            db = new ApplicationDbContext();
            string dateFromVerified = dateFrom ?? "";
            string dateToVerified = dateTo ?? "";
            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;
            ViewBag.stepIndex = stepIndex ?? 0;
            int reportVerifiedId = reportId ?? -1;

            var currentUser = db.Users.Where(x => x.UserName == User.Identity.Name).First();
            var themes = db.ThemeOfScientificWork
                .Where(x => x.Cathedra.Faculty.ID == currentUser.Cathedra.Faculty.ID).ToList();
            ViewBag.ScientificThemesByFaculty = themes.Select(x => new SelectListItem
            {
                Text = x.Value,
                Value = x.ID.ToString(),
            }).ToList();
            Report oldReport;
            if (reportVerifiedId == -1)
            {
                oldReport = db.Reports.Where(x => !x.IsSigned && x.User.UserName == User.Identity.Name).FirstOrDefault();
            } else
            {
                oldReport = db.Reports.Find(reportVerifiedId);
            }
            var allPublications = db.Publication.Where(x => x.User.Any(y => y.UserName == User.Identity.Name)).ToList();
            allPublications = allPublications.Where(x => !x.AcceptedToPrintPublicationReport.Union(x.RecomendedPublicationReport).Union(x.PrintedPublicationReport).Any(y => y.IsSigned || y.IsConfirmed)).ToList();            
            if (oldReport != null && dateFromVerified == "" && dateToVerified == "")
            {
                return ChooseOldReport(oldReport, allPublications);
            }

            var publicationOptions = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = false,
                        Id = x.ID,
                        Name = x.Name
                    };
                    if ((dateFromVerified == "" || (dateFromVerified != "" && Convert.ToDateTime(x.Date) >= DateTime.Parse(dateFromVerified))) &&
                    (dateToVerified == "" || (dateToVerified != "" && Convert.ToDateTime(x.Date) <= DateTime.Parse(dateToVerified))))
                    {
                        option.Checked = true;
                    }
                    return option;
                })
                .ToList();
            return View(new ReportViewModel()
            {
                ID = oldReport?.ID,
                PrintedPublication = publicationOptions,
                RecomendedPublication = publicationOptions,
                AcceptedToPrintPublication = publicationOptions
            });
        }

        [HttpPost]
        public ActionResult Update(ReportViewModel reportViewModel, int? stepIndex, int? oldIndex)
        {
            CreateOrUpdateReport(reportViewModel, oldIndex ?? 0);
            if (stepIndex == 4)
            {
                //checks if protocol or date values is empty. If it is, we will redirect user to page where he can set these values.
                if (reportViewModel.Protocol is null || reportViewModel.Date is null)
                {
                    int indexOfDateAProtocol = 3;
                    return RedirectToAction("Index", new { stepIndex = indexOfDateAProtocol, reportId = reportViewModel.ID });
                }
            }
            return RedirectToAction("Index", new { stepIndex = stepIndex, reportId = reportViewModel.ID });
        }

        public ActionResult Preview(int reportId)
        {
            return Content(reportService.GenerateHTMLReport(reportId));
        }

        public ActionResult PreviewPdf(int reportId)
        {
            return new ActionAsPdf("Preview", new { reportId = reportId});
        }

        public ActionResult GetLatex(int reportId)
        {
            String content = reportService.GenerateHTMLReport(reportId);
            var file = Path.Combine("D://test.html");
            System.IO.File.WriteAllText(file, content);
            String result = "";
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C://Users//maryc_1mz6n6s//AppData//Local//Pandoc//pandoc.exe",
                    Arguments = @"--from html D://test.html --to latex -s --wrap=preserve",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            int i = 0;
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                result += line;
                result += "\n";
                i++;
                if (i == 8)
                {
                    result += @"\usepackage[ukrainian]{babel}" + "\n";
                }
            }

            return File(System.Text.Encoding.GetEncoding(866).GetBytes(result), "application/x-latex", "report.latex");
        }

        private void CreateOrUpdateReport(ReportViewModel reportViewModel, int stepIndex)
        {
            var allPublications = db.Publication.ToList();
            if (reportViewModel.ID == null && !db.Reports.Any(x => x.ID == reportViewModel.ID))
            {
                var reportToCreate = ReportConverter.ConvertToEntity(reportViewModel);
                reportToCreate.User = db.Users.Find(User.Identity.GetUserId());
                reportToCreate.ThemeOfScientificWork = db.ThemeOfScientificWork.Where(x => x.ID == reportViewModel.ThemeOfScientificWorkId).FirstOrDefault();
                reportToCreate.PrintedPublication = allPublications.Where(x => reportViewModel.PrintedPublication.Any(y => y.Id == x.ID && y.Checked)).ToList();
                reportToCreate.RecomendedPublication = allPublications.Where(x => reportViewModel.RecomendedPublication.Any(y => y.Id == x.ID && y.Checked)).ToList();
                reportToCreate.AcceptedToPrintPublication = allPublications.Where(x => reportViewModel.AcceptedToPrintPublication.Any(y => y.Id == x.ID && y.Checked)).ToList();
                db.Reports.Add(reportToCreate);
                db.SaveChanges();
            }
            else
            {
                var report = db.Reports.Find(reportViewModel.ID);
                switch (stepIndex)
                {
                    case 0:
                        report.PrintedPublication = allPublications.Where(x => reportViewModel.PrintedPublication.Any(y => y.Id == x.ID && y.Checked)).ToList();
                        report.RecomendedPublication = allPublications.Where(x => reportViewModel.RecomendedPublication.Any(y => y.Id == x.ID && y.Checked)).ToList();
                        report.AcceptedToPrintPublication = allPublications.Where(x => reportViewModel.AcceptedToPrintPublication.Any(y => y.Id == x.ID && y.Checked)).ToList();
                        break;
                    case 1:
                        report.ThemeOfScientificWork = db.ThemeOfScientificWork.Where(x => x.ID == reportViewModel.ThemeOfScientificWorkId).FirstOrDefault();
                        report.ThemeOfScientificWorkDescription = reportViewModel.ThemeOfScientificWorkDescription;
                        break;
                    case 2:
                        report.MembershipInCouncils = reportViewModel.MembershipInCouncils;
                        report.Other = reportViewModel.Other;
                        report.ParticipationInGrands = reportViewModel.ParticipationInGrands;
                        report.PatentForInevention = reportViewModel.PatentForInevention;
                        report.ApplicationForInevention = reportViewModel.ApplicationForInevention;
                        report.ReviewForTheses = reportViewModel.ReviewForTheses;
                        report.ScientificControlDoctorsWork = reportViewModel.ScientificControlDoctorsWork;
                        report.ScientificControlStudentsWork = reportViewModel.ScientificControlStudentsWork;
                        report.ScientificTrainings = reportViewModel.ScientificTrainings;
                        break;
                    case 3:
                        report.Date = reportViewModel.Date;
                        report.Protocol = reportViewModel.Protocol;
                        break;
                    default:
                        return;
                }
                db.SaveChanges();
            }
        }

        private ActionResult ChooseOldReport(Report oldReport, List<Publication> allPublications)
        {
            var viewModel = ReportConverter.ConvertToViewModel(oldReport);
            viewModel.RecomendedPublication = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = false,
                        Id = x.ID,
                        Name = x.Name
                    };
                    if (viewModel.RecomendedPublication.Any(y => y.Id == x.ID))
                    {
                        option.Checked = true;
                    }
                    return option;
                })
                .ToList();
            viewModel.PrintedPublication = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = false,
                        Id = x.ID,
                        Name = x.Name
                    };
                    if (viewModel.PrintedPublication.Any(y => y.Id == x.ID))
                    {
                        option.Checked = true;
                    }
                    return option;
                })
                .ToList();
            viewModel.AcceptedToPrintPublication = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = false,
                        Id = x.ID,
                        Name = x.Name
                    };
                    if (viewModel.AcceptedToPrintPublication.Any(y => y.Id == x.ID))
                    {
                        option.Checked = true;
                    }
                    return option;
                })
                .ToList();
            return View(viewModel);
        }
    }
}
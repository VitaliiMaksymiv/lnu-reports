using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models.db;
using UserManagement.Models.Reports;

namespace UserManagement.Converter
{
    public class ReportConverter
    {

        public static ReportViewModel ConvertToViewModel(Report report)
        {
            var viewModel = new ReportViewModel()
            {
                ID = report.ID,
                ParticipationInGrands = report.ParticipationInGrands,
                ScientificTrainings = report.ScientificTrainings,
                ScientificControlDoctorsWork = report.ScientificControlDoctorsWork,
                ScientificControlStudentsWork = report.ScientificControlStudentsWork,
                ApplicationForInevention = report.ApplicationForInevention,
                ThemeOfScientificWorkDescription = report.ThemeOfScientificWorkDescription,
                PatentForInevention = report.PatentForInevention,
                ReviewForTheses = report.ReviewForTheses,
                MembershipInCouncils = report.MembershipInCouncils,
                Other = report.Other,
                Protocol = report.Protocol,
                Date = report.Date,
                IsSigned = report.IsSigned,
                IsConfirmed = report.IsConfirmed,
                ThemeOfScientificWorkId = report.ThemeOfScientificWork?.ID,
            };

            viewModel.PrintedPublication = report.PrintedPublication.Select(x => new PublicationOption() { Id = x.ID, Checked = true, Name = x.Name }).ToList();
            viewModel.AcceptedToPrintPublication = report.AcceptedToPrintPublication.Select(x => new PublicationOption() { Id = x.ID, Checked = true, Name = x.Name }).ToList();
            viewModel.RecomendedPublication = report.RecomendedPublication.Select(x => new PublicationOption() { Id = x.ID, Checked = true, Name = x.Name }).ToList();

            return viewModel;
        }

        public static Report ConvertToEntity(ReportViewModel reportViewModel)
        {
            var report = new Report()
            {
                ParticipationInGrands = reportViewModel.ParticipationInGrands,
                ScientificTrainings = reportViewModel.ScientificTrainings,
                ScientificControlDoctorsWork = reportViewModel.ScientificControlDoctorsWork,
                ScientificControlStudentsWork = reportViewModel.ScientificControlStudentsWork,
                ApplicationForInevention = reportViewModel.ApplicationForInevention,
                ThemeOfScientificWorkDescription = reportViewModel.ThemeOfScientificWorkDescription,
                PatentForInevention = reportViewModel.PatentForInevention,
                ReviewForTheses = reportViewModel.ReviewForTheses,
                MembershipInCouncils = reportViewModel.MembershipInCouncils,
                Other = reportViewModel.Other,
                Protocol = reportViewModel.Protocol,
                Date = reportViewModel.Date,
                IsSigned = reportViewModel.IsSigned,
                IsConfirmed = reportViewModel.IsConfirmed,
            };

            return report;
        }
    }
}
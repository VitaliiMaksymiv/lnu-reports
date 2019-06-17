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



        public static CathedraReportViewModel ConvertToViewModel(CathedraReport report)
        {
            var viewModel = new CathedraReportViewModel()
            {
                ID = report.ID,
                Protocol = report.Protocol,
                Date = report.Date,
                AchivementSchool = report.AchivementSchool,
                AllDescriptionBudgetTheme = report.AllDescriptionBudgetTheme,
                AllDescriptionHospDohovirTheme = report.AllDescriptionHospDohovirTheme,
                AllDescriptionThemeInWorkTime = report.AllDescriptionThemeInWorkTime,
                ApplicationAndPatentsOnInventionBudgetTheme = report.ApplicationAndPatentsOnInventionBudgetTheme,
                ApplicationAndPatentsOnInventionHospDohovirTheme = report.ApplicationAndPatentsOnInventionHospDohovirTheme,
                ApplicationAndPatentsOnInventionThemeInWorkTime = report.ApplicationAndPatentsOnInventionThemeInWorkTime,
                ApplicationOnInvention = report.ApplicationOnInvention,
                BudgetThemeId = report.BudgetTheme?.ID,
                ConferencesInUniversity = report.ConferencesInUniversity,
                CooperationWithAcadamyOfScience = report.CooperationWithAcadamyOfScience,
                CooperationWithForeignScientificInstitution = report.CooperationWithForeignScientificInstitution,
                CVBudgetTheme = report.CVBudgetTheme,
                CVHospDohovirTheme = report.CVHospDohovirTheme,
                CVThemeInWorkTime = report.CVThemeInWorkTime,
                DefenseOfCoworkers = report.DefenseOfCoworkers,
                DefenseOfDoctorantsAndAspirants = report.DefenseOfDoctorantsAndAspirants,
                DefensesOfCoworkersBudgetTheme = report.DefensesOfCoworkersBudgetTheme,
                DefensesOfCoworkersHospDohovirTheme = report.DefensesOfCoworkersHospDohovirTheme,
                DefensesOfCoworkersThemeInWorkTime = report.DefensesOfCoworkersThemeInWorkTime,
                DefenseWithSpecialPeople = report.DefenseWithSpecialPeople,
                HospDohovirThemeId = report.HospDohovirTheme?.ID,
                Materials = report.Materials,
                OtherBudgetTheme = report.OtherBudgetTheme,
                OtherFormsOfScientificWork = report.OtherFormsOfScientificWork,
                OtherHospDohovirTheme = report.OtherHospDohovirTheme,
                OtherThemeInWorkTime = report.OtherThemeInWorkTime,
                Patents = report.Patents,
                PropositionForNewForms = report.PropositionForNewForms,
                StudentsWorks = report.StudentsWorks,
                ThemeInWorkTimeId = report.ThemeInWorkTime?.ID
            };

            viewModel.PrintedPublicationBudgetTheme = report.PrintedPublicationBudgetTheme.Select(x => new PublicationOption() { Id = x.ID, Checked = false, Name = x.Name }).ToList();
            viewModel.PrintedPublicationHospDohovirTheme = report.PrintedPublicationHospDohovirTheme.Select(x => new PublicationOption() { Id = x.ID, Checked = false, Name = x.Name }).ToList();
            viewModel.PrintedPublicationThemeInWorkTime = report.PrintedPublicationThemeInWorkTime.Select(x => new PublicationOption() { Id = x.ID, Checked = false, Name = x.Name }).ToList();

            return viewModel;
        }


        public static CathedraReport ConvertToEntity(CathedraReportViewModel reportViewModel)
        {
            var report = new CathedraReport()
            {
                Protocol = reportViewModel.Protocol,
                Date = reportViewModel.Date,
                AchivementSchool = reportViewModel.AchivementSchool,
                AllDescriptionBudgetTheme = reportViewModel.AllDescriptionBudgetTheme,
                AllDescriptionHospDohovirTheme = reportViewModel.AllDescriptionHospDohovirTheme,
                AllDescriptionThemeInWorkTime = reportViewModel.AllDescriptionThemeInWorkTime,
                ApplicationAndPatentsOnInventionBudgetTheme = reportViewModel.ApplicationAndPatentsOnInventionBudgetTheme,
                ApplicationAndPatentsOnInventionHospDohovirTheme = reportViewModel.ApplicationAndPatentsOnInventionHospDohovirTheme,
                ApplicationAndPatentsOnInventionThemeInWorkTime = reportViewModel.ApplicationAndPatentsOnInventionThemeInWorkTime,
                ApplicationOnInvention = reportViewModel.ApplicationOnInvention,
                ConferencesInUniversity = reportViewModel.ConferencesInUniversity,
                CooperationWithAcadamyOfScience = reportViewModel.CooperationWithAcadamyOfScience,
                CooperationWithForeignScientificInstitution = reportViewModel.CooperationWithForeignScientificInstitution,
                CVBudgetTheme = reportViewModel.CVBudgetTheme,
                CVHospDohovirTheme = reportViewModel.CVHospDohovirTheme,
                CVThemeInWorkTime = reportViewModel.CVThemeInWorkTime,
                DefenseOfCoworkers = reportViewModel.DefenseOfCoworkers,
                DefenseOfDoctorantsAndAspirants = reportViewModel.DefenseOfDoctorantsAndAspirants,
                DefensesOfCoworkersBudgetTheme = reportViewModel.DefensesOfCoworkersBudgetTheme,
                DefensesOfCoworkersHospDohovirTheme = reportViewModel.DefensesOfCoworkersHospDohovirTheme,
                DefensesOfCoworkersThemeInWorkTime = reportViewModel.DefensesOfCoworkersThemeInWorkTime,
                DefenseWithSpecialPeople = reportViewModel.DefenseWithSpecialPeople,
                Materials = reportViewModel.Materials,
                OtherBudgetTheme = reportViewModel.OtherBudgetTheme,
                OtherFormsOfScientificWork = reportViewModel.OtherFormsOfScientificWork,
                OtherHospDohovirTheme = reportViewModel.OtherHospDohovirTheme,
                OtherThemeInWorkTime = reportViewModel.OtherThemeInWorkTime,
                Patents = reportViewModel.Patents,
                PropositionForNewForms = reportViewModel.PropositionForNewForms,
                StudentsWorks = reportViewModel.StudentsWorks,
            };

            return report;
        }
    }
}
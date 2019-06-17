using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserManagement.Models.db;

namespace UserManagement.Models.Reports
{
    public class CathedraReportViewModel
    {
        public int? ID { get; set; }
        public String AchivementSchool { get; set; } //1

        public int? BudgetThemeId { get; set; }//2
        public String AllDescriptionBudgetTheme { get; set; }
        public String CVBudgetTheme { get; set; }//2.1
        public String DefensesOfCoworkersBudgetTheme { get; set; }//2.2
        public List<PublicationOption> PrintedPublicationBudgetTheme { get; set; }//2.3
        public String ApplicationAndPatentsOnInventionBudgetTheme { get; set; }//2.4
        public String OtherBudgetTheme { get; set; }//2.5

        public int? ThemeInWorkTimeId { get; set; }//3
        public String AllDescriptionThemeInWorkTime { get; set; }
        public String CVThemeInWorkTime { get; set; }//3.1
        public String DefensesOfCoworkersThemeInWorkTime { get; set; }//3.2
        public List<PublicationOption> PrintedPublicationThemeInWorkTime { get; set; }//3.3
        public String ApplicationAndPatentsOnInventionThemeInWorkTime { get; set; }//3.4
        public String OtherThemeInWorkTime { get; set; }//3.5

        public int? HospDohovirThemeId { get; set; }//4
        public String AllDescriptionHospDohovirTheme { get; set; }
        public String CVHospDohovirTheme { get; set; }//4.1
        public String DefensesOfCoworkersHospDohovirTheme { get; set; }//4.2
        public List<PublicationOption> PrintedPublicationHospDohovirTheme { get; set; }//4.3
        public String ApplicationAndPatentsOnInventionHospDohovirTheme { get; set; }//4.4
        public String OtherHospDohovirTheme { get; set; }//4.5

        public String OtherFormsOfScientificWork { get; set; }//5

        public String CooperationWithAcadamyOfScience { get; set; } //6.1
        public String CooperationWithForeignScientificInstitution { get; set; } //6.2

        public List<CathedraDefenses> DefenseOfDoctorantsAndAspirants { get; set; }//7.1
        public List<CoworkersDefenses> DefenseOfCoworkers { get; set; }//7.2
        public List<OtherDefenses> DefenseWithSpecialPeople { get; set; }//7.3

        public String StudentsWorks { get; set; }//8

        public String ConferencesInUniversity { get; set; } //10

        public String ApplicationOnInvention { get; set; }//11.1
        public String Patents { get; set; }//11.2

        public String Materials { get; set; }//12

        public String PropositionForNewForms { get; set; }//13

        public String Protocol { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }
    }
}
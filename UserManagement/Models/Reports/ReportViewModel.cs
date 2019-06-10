using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UserManagement.Models.db;

namespace UserManagement.Models.Reports
{
    public class ReportViewModel
    {
        public int? ID { get; set; }
        public String ParticipationInGrands { get; set; }
        public String ScientificTrainings { get; set; }
        public String ScientificControlDoctorsWork { get; set; }
        public String ScientificControlStudentsWork { get; set; }
        public String ApplicationForInevention { get; set; }
        public String PatentForInevention { get; set; }
        public String ReviewForTheses { get; set; }
        public String MembershipInCouncils { get; set; }
        public String Other { get; set; }
        public String ThemeOfScientificWorkDescription { get; set; }
        public Boolean IsSigned { get; set; }
        public Boolean IsConfirmed { get; set; }

        public String Protocol { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        public int? ThemeOfScientificWorkId { get; set; }
        public List<PublicationOption> PrintedPublication { get; set; } = new List<PublicationOption>();
        public List<PublicationOption> RecomendedPublication { get; set; } = new List<PublicationOption>();
        public List<PublicationOption> AcceptedToPrintPublication { get; set; } = new List<PublicationOption>();
    }

    public class PublicationOption
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Boolean Checked { get; set; }
    }
}
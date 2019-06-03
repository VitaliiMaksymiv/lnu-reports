using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserManagement.Models.db
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
        public String ParticipationInGrands { get; set; } // Пункт 2
        public String ScientificTrainings { get; set; } // Пункт 3
        public String ScientificControlDoctorsWork { get; set; } // Пункт 4
        public String ScientificControlStudentsWork { get; set; }// Пункт 5
        public String ApplicationForInevention { get; set; } // Пункт 7.1
        public String PatentForInevention { get; set; }// Пункт 7.2
        public String ReviewForTheses { get; set; }// Пункт 8
        public String MembershipInCouncils { get; set; } // Пункт 9
        public String Other { get; set; }// Пункт 10
        public String ThemeOfScientificWorkDescription { get; set; }// Пункт 1

        public String Protocol { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        public virtual ThemeOfScientificWork ThemeOfScientificWork { get; set; }// Пункт 1
        public virtual ApplicationUser User { get; set; }

        [InverseProperty("PrintedPublicationReport")]
        public virtual ICollection<Publication> PrintedPublication { get; set; }// Пункт 6.1
        [InverseProperty("RecomendedPublicationReport")]
        public virtual ICollection<Publication> RecomendedPublication { get; set; }// Пункт 6.2
        [InverseProperty("AcceptedToPrintPublicationReport")]
        public virtual ICollection<Publication> AcceptedToPrintPublication { get; set; }// Пункт 6.2.5
    }
}
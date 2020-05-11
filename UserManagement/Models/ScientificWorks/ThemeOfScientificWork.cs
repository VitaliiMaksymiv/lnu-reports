using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserManagement.Models.db
{
    public class ThemeOfScientificWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
        public String Value { get; set; }
        public String ThemeNumber { get; set; }
        public String ScientificHead { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodTo { get; set; }
        public Financial Financial { get; set; }

        public virtual ICollection<Report> Report { get; set; }
        public virtual Cathedra Cathedra { get; set; }
    }

    public enum Financial
    {
        БЮДЖЕТ,
        ГОСПДОГОВІР,
        В_МЕЖАХ_РОБОЧОГО_ЧАСУ
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserManagement.Models.Reports
{
    public class CathedraDefenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        public String SurnameAndInitials { get; set; }
        public String ScientificHead { get; set; }
        public Int32 YearOfEnd { get; set; }
        public DateTime DateOfInning { get; set; }
        public DateTime DateOfDefense { get; set; }
        public String ThemeOfWork { get; set; }
    }
}
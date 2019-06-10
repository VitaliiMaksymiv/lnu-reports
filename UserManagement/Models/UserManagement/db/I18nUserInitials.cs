using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserManagement.Models.db
{
    public class I18nUserInitials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
        public Language Language { get; set; }

        public String FirstName { get; set; } = "";
        public String LastName { get; set; } = "";
        public String FathersName { get; set; } = "";


        public virtual ApplicationUser User { get; set; }
    }

    public enum Language
    {
        UA,
        EN,
    }
}
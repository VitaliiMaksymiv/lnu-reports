using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UserManagement.Models.PublicationDB;

namespace UserManagement.Models.db
{
    public class Publication
    {
        public Publication()
        {
            this.User = new HashSet<ApplicationUser>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String OtherAuthors { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime Date { get; set; }
        public Double SizeOfPages { get; set; }
        public PublicationType PublicationType { get; set; }

        public virtual ICollection<ApplicationUser> User { get; set; }
    }
}
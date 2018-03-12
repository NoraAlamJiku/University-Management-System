using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Name")]
        [Index("Ix_Name", IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class GradeLetter
    {
        [Key]
        public int Id { get; set; }

    
        public String Grade { get; set; }

        public virtual ICollection<StudentResult> StudentResults { get; set; } 
    }
}
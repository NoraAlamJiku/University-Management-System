using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Designation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Designation")]
        public string Name { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; } 
    }
}
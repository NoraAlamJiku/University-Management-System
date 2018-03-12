using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class RoomNo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoomNumber { get; set; }

        public virtual ICollection<AllocateClassroom> AllocateClassrooms { get; set; } 
    }
}
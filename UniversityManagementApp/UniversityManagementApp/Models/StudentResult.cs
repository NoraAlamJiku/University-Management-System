using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class StudentResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Student Reg. No")]
        public int RegisterStudentId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Email")]
        public String Email { get; set; }

        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public string Department { get; set; }

        [Required]
        [DisplayName("Select Course")]
        public int CourseId { get; set; }

        [Required]
        [DisplayName("Select Grade Letter")]
        public int GradeLetterId { get; set; }

        public virtual Course Course { get; set; }
        public virtual GradeLetter GradeLetter { get; set; }
        public virtual RegisterStudent RegisterStudent { get; set; } 
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementApp.Models
{
    public class AssignToTeacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required]
        [DisplayName("Teacher")]
       // [Remote("IsTeacherExist", "AssignToTeachers", ErrorMessage = "Already exists")]
        public int TeacherId { get; set; }

        [Required]
        [DisplayName("Credit To Be Taken")]
        public double CreditToBeTaken { get; set; }

        [Required]
        [DisplayName("Remaining Credit")]
        public double? RemainingCredit { get; set; }

        [Required]
        [DisplayName("Course Code")]
        [Remote("IsCourseExist", "AssignToTeachers", ErrorMessage = "Already exists")]
        public int CourseId { get; set; }


        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Credit")]
        public string Credit { get; set; }

        public virtual Department Department { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementApp.Models
{
    public partial class Course
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(10, MinimumLength = 5)]
        [DisplayName("Code")]
        [Index("Ic_Code", IsUnique = true)]
        [Remote("IsCodeExist", "Courses", ErrorMessage = "This code already exists!")]
        public string Code { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DisplayName("Name")]
        [Index("Ix_Name", IsUnique = true)]
        [Remote("IsNameExist", "Courses", ErrorMessage = "This name already exists!")]
        public string Name { get; set; }

        [Required]
        [Range(0.5, 5.0)]
        [DisplayName("Credit")]
        public double Credit { get; set; }


        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }


        [Required]
        //[DisplayName("Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }

        public bool CourseStatus { get; set; }
        public int TeacherId { get; set; }


        public virtual Department Department { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual ICollection<AssignToTeacher> AssignToTeachers { get; set; }
        public virtual ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
        public virtual ICollection<EnrollCourse> EnrollCourses { get; set; }
        public virtual ICollection<StudentResult> StudentResults { get; set; } 
    }

}
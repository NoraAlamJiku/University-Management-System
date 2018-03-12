using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public partial class EnrollCourse
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
        public string Email { get; set; }

        [Required]
        [DisplayName("Department")]
        public string Department { get; set; }

        [Required]
        [DisplayName("Select Course")]
        public int CourseId { get; set; }

        [Required]
        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public virtual Course Course { get; set; }
        public virtual RegisterStudent RegisterStudent { get; set; } 
    }
    public partial class EnrollCourse : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            UniversityDbContext db = new UniversityDbContext();

            var dbModel = db.EnrollCourses.FirstOrDefault(x => x.RegisterStudentId == RegisterStudentId && x.CourseId == CourseId);
          
            if (dbModel != null)
            {
                ValidationResult error = new ValidationResult("Alredy Assign", new[] { "RegisterStudentId", "CourseId" });
                yield return error;
            }
       
        }
    }
}
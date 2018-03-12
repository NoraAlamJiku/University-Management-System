using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public partial class RegisterStudent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("Email")]
        [StringLength(50, MinimumLength = 5)]
        [Index("Ix_Email", IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 4)]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [ReadOnly(true)]
        public string RegNo { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<EnrollCourse> EnrollCourses { get; set; }
        public virtual ICollection<StudentResult> StudentResults { get; set; }
    }

    public partial class RegisterStudent : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            UniversityDbContext db = new UniversityDbContext();
            Email = Email;
            var dbModel = db.RegisterStudents.FirstOrDefault(x => x.Email == Email);
            if (dbModel != null)
            {
                ValidationResult error = new ValidationResult("This Email Adders Already exists", new[] { "Email" });
                yield return error;
            }
        }
    }
}
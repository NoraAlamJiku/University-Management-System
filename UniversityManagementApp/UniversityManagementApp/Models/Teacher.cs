using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public partial class Teacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 4)]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50, MinimumLength = 5)]
        [Index("Ix_Email", IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }

        [Required]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }

        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [DisplayName("Credit To Be Taken")]
        public double CreditToBeTaken { get; set; }

        public virtual Department Department { get; set; }

        public virtual Designation Designation { get; set; }
        public virtual ICollection<AssignToTeacher> AssignToTeachers { get; set; } 
    }

    public partial class Teacher : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            UniversityDbContext db = new UniversityDbContext();
            Email = Email;
            var dbModel = db.Teachers.FirstOrDefault(x => x.Email == Email);
            if (dbModel != null)
            {
                ValidationResult error = new ValidationResult("This Email Adders Already exists", new[] { "Email" });
                yield return error;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public partial class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 2)]
        [DisplayName("Code")]
        [Index("Ix_Code", IsUnique = true)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Name")]
        [Index("Ix_Name", IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<AssignToTeacher> AssignToTeachers { get; set; }
       
        public virtual ICollection<RegisterStudent> RegisterStudents { get; set; }
        public virtual ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
       
    }

    public partial class Department : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            UniversityDbContext db=new UniversityDbContext();
            var dbModel = db.Departments.FirstOrDefault(x => x.Code.ToUpper() == Code.ToUpper());
            var dbModel1 = db.Departments.FirstOrDefault(x => x.Name.ToUpper() == Name.ToUpper());
            if (dbModel!=null)
            {
                ValidationResult error = new ValidationResult("Code Already exists", new[] {"Code"});
                yield return error;
            }
            else if (dbModel1 != null)
            {
                ValidationResult error = new ValidationResult("Name Already exists", new[] { "Name" });
                yield return error;
            }
        }
    }
}
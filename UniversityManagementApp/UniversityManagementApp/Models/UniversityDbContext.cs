using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<AssignToTeacher> AssignToTeachers { get; set; }
      
        public DbSet<RegisterStudent> RegisterStudents { get; set; }
        public DbSet<AllocateClassroom> AllocateClassrooms { get; set; }
        public DbSet<RoomNo> RoomNos { get; set; }
        public DbSet<DayName> DayNames { get; set; }
     
        public DbSet<EnrollCourse> EnrollCourses { get; set; }
        public DbSet<StudentResult> StudentResults { get; set; }
        public DbSet<GradeLetter> GradeLetters { get; set; }
      



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        //    modelBuilder.Entity<Department>()
        //       .HasRequired(f => f.Courses)
        //       .WithRequiredDependent()
        //       .WillCascadeOnDelete(false);

        //}
    }
}
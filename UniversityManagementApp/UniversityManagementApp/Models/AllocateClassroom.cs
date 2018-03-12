using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class AllocateClassroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required]
        [DisplayName("Course")]
        public int CourseId { get; set; }

        [Required]
        [DisplayName("Room No")]
        public int RoomNoId { get; set; }

        [Required]
        [DisplayName("Day")]
        public int DayNameId { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public double StartTime { get; set; }
        public TimeSpan StartTime1 { get; set; }

        [Required]
        [DisplayName("End Time")]
        public double EndTime { get; set; }
        public TimeSpan EndTime1 { get; set; }

        public string RoomStatus { get; set; }

        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
        public virtual DayName DayName { get; set; }
        public virtual RoomNo RoomNo { get; set; } 
    }

}
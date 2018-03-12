using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.ViewModels
{
    public class ClassSchdual
    {
        public ClassSchdual()
        {
            
        }
        public ClassSchdual(AllocateClassroom room)
        {
            code = room.Course.Code;
            Name = room.Course.Name;
            Schedul = room.RoomNo.RoomNumber + " " + room.DayName.Name + " " + room.StartTime + " " + room.EndTime;
        }
        public string code { get; set; }
        public string Name { get; set; }
        public string Schedul { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Manager
{
    public class RegisterStudentManager
    {
        private UniversityDbContext db = new UniversityDbContext();

        public string GetStudentRegNo(RegisterStudent aRegisterStudent)
        {
            Department aDepartment = db.Departments.FirstOrDefault(x => x.Id == aRegisterStudent.DepartmentId);
            int countDepartmentSId =
                db.RegisterStudents.Count(
                    x =>
                        (x.DepartmentId == aRegisterStudent.DepartmentId) &&
                        (x.DueDate.Year == aRegisterStudent.DueDate.Year)) + 1;
            int ZeroToBeAdded = 3 - countDepartmentSId.ToString().Length;
            string number = "";
            for (int i = 0; i < ZeroToBeAdded; i++)
            {
                number += 0;
            }
            return aDepartment.Code + "-" + aRegisterStudent.DueDate.Year + "-" + number + countDepartmentSId;

        }


        //private string GetStudentRegNo(Student aStudent)
        //{

        //    Department aDepartment = db.Departments.FirstOrDefault(aDept => aDept.DeptId == aStudent.DeptId);
        //    int countDeptStd =
        //        db.Students.Count(aStd => (aStd.DeptId == aStudent.DeptId) && (aStd.RegDate.Year == aStudent.RegDate.Year)) + 1;
        //    int noOfZeroToBeAdded = 3 - countDeptStd.ToString().Length;
        //    string noOfZero = "";
        //    for (int i = 0; i < noOfZeroToBeAdded; i++)
        //    {
        //        noOfZero += "0";
        //    }

        //    return aDepartment.DeptCode + "-" + aStudent.RegDate.Year + "-" + noOfZero + countDeptStd;

        //}
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class CoursesController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Index()
        {
            ViewBag.departments = db.Departments.ToList();
         
            return View();
        }

        public JsonResult GetNameByRegNo(int departmentId)
        {
            return Json(db.AssignToTeachers.Where(x => x.DepartmentId == departmentId).Select(x => new
            {
                Id = x.Id,
                Code = x.Course.Code,
                name = x.Course.Name,
                semester = x.Course.Semester.Name,
                assignTo = x.Teacher.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UnallocatedClassroom()
        {
            return View();
        }
        public JsonResult UnAllocateAllRooms(bool decision)
        {
            var roomStatus = db.AllocateClassrooms.Where(m => m.RoomStatus == "Allocated").ToList();
            if (roomStatus.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var room in roomStatus)
                {
                    room.RoomStatus = "NotAllocated";
                    db.AllocateClassrooms.AddOrUpdate(room);
                    db.SaveChanges();

                }
                return Json(true);
            }

        }
        public ActionResult UnAssignCourse()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UnassignAllCourses(bool decision)
        {
            var courses = db.Courses.Where(m => m.CourseStatus == true).ToList();
            var assigntoTeacher = db.AssignToTeachers.ToList();
            if (courses.Count == 0)
            {
                return Json(false);
            }
            else
            {
                foreach (var course in courses)
                {
                    course.CourseStatus = false;
                    course.TeacherId = 0;
                    db.Courses.AddOrUpdate(course);
                    db.SaveChanges();


                }
                foreach (var assignto in assigntoTeacher)
                {
                    assignto.RemainingCredit = 0;
                    db.AssignToTeachers.AddOrUpdate(assignto);
                    db.SaveChanges();


                }
                return Json(true);

          }
        }
        public JsonResult ShowCourseStatistics(int deptId)
        {
            var courses = db.Courses.Where(x => x.DepartmentId == deptId);
            return Json(db.Courses.Where(x => x.DepartmentId == deptId).Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                name = x.Name,
                stastu = x.CourseStatus,
                semester = x.Semester.Name,
                assignTo = x.AssignToTeachers.FirstOrDefault().Teacher.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Credit,Description,DepartmentId,SemesterId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                ViewBag.Msg = "Course Saved Successfull.";
               // return RedirectToAction("Create");
            }
            
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", course.SemesterId);
            return View(course);
        }

        public JsonResult IsNameExist(string Name)
        {
            bool a = db.Courses.ToList().Exists(c => c.Name == Name);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCodeExist(string Code)
        {
            bool a = db.Courses.ToList().Exists(c => c.Code == Code);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

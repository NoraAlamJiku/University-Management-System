using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Models;
using UniversityManagementApp.Report;

namespace UniversityManagementApp.Controllers
{
    public class StudentResultsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: StudentResults
        public ActionResult Index()
        {
            ViewBag.departments = db.RegisterStudents.ToList();
            var studentResults = db.StudentResults.Include(s => s.Course).Include(s => s.GradeLetter).Include(s => s.RegisterStudent);
           // return View(studentResults.ToList());
            return View();
        }
        [HttpPost]
        public ActionResult Index(StudentResult studentResult)
        {
           // ViewBag.departments = db.RegisterStudents.ToList();
            //var studentResults = db.StudentResults.Include(s => s.Course).Include(s => s.GradeLetter).Include(s => s.RegisterStudent);
            // return View(studentResults.ToList());
            //report(studentResult);
            StudentReport studentReport = new StudentReport();
            var student = studentResult1(studentResult);
            byte[] abytes = studentReport.PrePareReport(student, studentResult);
            return File(abytes, "application/pdf");
            // return View();
        }
        //public ActionResult report(StudentResult studentResult)
        //{
        //    StudentReport studentReport = new StudentReport();
        //    byte[] abytes = studentReport.PrePareReport(studentResult1(studentResult));
        //    return File(abytes, "application/pdf");

        //}

        public List<StudentResult> studentResult1(StudentResult studentResult)
        {
            var ressult = db.StudentResults.Where(x => x.RegisterStudentId == studentResult.RegisterStudentId).ToList();
            return ressult;
        }
        // GET: StudentResults/Details/5
        //public JsonResult GetStudentById(int studentRegNo)
        //{
        //    var students = db.RegisterStudents.Where(m => m.Id == studentRegNo).ToList();
        //    return Json(students, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetCoursesbyRegNo(int regNo)
        {
            var courses = db.EnrollCourses.Where(m => m.RegisterStudentId == regNo).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResult studentResult = db.StudentResults.Find(id);
            if (studentResult == null)
            {
                return HttpNotFound();
            }
            return View(studentResult);
        }

        // GET: StudentResults/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code");
            ViewBag.GradeLetterId = new SelectList(db.GradeLetters, "Id", "Grade");
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "RegNo");
            return View();
        }

     

        public JsonResult GetNameByRegNo(int departmentId)
        {
            return
                Json(
                    db.RegisterStudents.Where(s => s.Id == departmentId)
                        .Select(s => new { Id = s.Id, Name = s.Name, Email = s.Email, DepartmentId = s.Department.Name })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetNameByRegNo(int departmentId)
        //{
        //    var students = db.RegisterStudents.Where(s => s.Id == departmentId).ToList();
        //    return Json(students, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetEmailByRegNo(int departmentId)
        {
            return
                Json(
                    db.RegisterStudents.Where(s => s.Id == departmentId)
                        .Select(s => new { Id = s.Id, Email = s.Email })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDepartmentNameByRegNo(int departmentId)
        {
            return
                Json(
                    db.RegisterStudents.Where(s => s.Id == departmentId)
                        .Select(s => new { Id = s.Id, DepartmentId = s.Department.Name })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        public JsonResult GetStudentResultByRegNo(int departmentId)
        {
            return Json(db.StudentResults.Where(x => x.RegisterStudentId == departmentId).Select(x => new
            {
                Id = x.Id,
                Code = x.Course.Code,
                name = x.Course.Name,
                grade = x.GradeLetter.Grade
           
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegisterStudentId,Name,Email,DepartmentId,CourseId,GradeLetterId")] StudentResult studentResult)
        {
            if (ModelState.IsValid)
            {
                var enrollCourses = db.StudentResults.Where(m => m.RegisterStudentId == studentResult.RegisterStudentId && m.CourseId == studentResult.CourseId).ToList();

                if (enrollCourses.Count() == 1)
                {
                    var id = enrollCourses[0].Id;
                   // var date = enrollCourses[0].EnrollDate;
                    studentResult.Id = id;
                   // studentResult.EnrollDate = date;
                    db.StudentResults.AddOrUpdate(studentResult);
                    db.SaveChanges();
                    ViewBag.Msg = "Update Result Successful!";
                }
                else
                {
                    db.StudentResults.Add(studentResult);
                    db.SaveChanges();
                    ViewBag.Msg = "Result Saved Successful!";
                }
                //db.StudentResults.Add(studentResult);
               
               // int aa = db.SaveChanges();
               // return RedirectToAction("Create");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", studentResult.CourseId);
            ViewBag.GradeLetterId = new SelectList(db.GradeLetters, "Id", "Grade", studentResult.GradeLetterId);
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "RegNo", studentResult.RegisterStudentId);
            return View(studentResult);
        }

        // GET: StudentResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResult studentResult = db.StudentResults.Find(id);
            if (studentResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", studentResult.CourseId);
            ViewBag.GradeLetterId = new SelectList(db.GradeLetters, "Id", "Grade", studentResult.GradeLetterId);
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "Name", studentResult.RegisterStudentId);
            return View(studentResult);
        }

        // POST: StudentResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegisterStudentId,Name,Email,DepartmentId,CourseId,GradeLetterId")] StudentResult studentResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", studentResult.CourseId);
            ViewBag.GradeLetterId = new SelectList(db.GradeLetters, "Id", "Grade", studentResult.GradeLetterId);
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "Name", studentResult.RegisterStudentId);
            return View(studentResult);
        }

        // GET: StudentResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentResult studentResult = db.StudentResults.Find(id);
            if (studentResult == null)
            {
                return HttpNotFound();
            }
            return View(studentResult);
        }

        // POST: StudentResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentResult studentResult = db.StudentResults.Find(id);
            db.StudentResults.Remove(studentResult);
            db.SaveChanges();
            return RedirectToAction("Index");
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class EnrollCoursesController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: EnrollCourses
        public ActionResult Index()
        {
            var enrollCourses = db.EnrollCourses.Include(e => e.Course).Include(e => e.RegisterStudent);
            return View(enrollCourses.ToList());
        }

        // GET: EnrollCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code");
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "RegNo");
            return View();
        }

        // POST: EnrollCourses/Create
        public ActionResult GetNameByRegNo(int departmentId)
        {
            return
                Json(
                    db.RegisterStudents.Where(s => s.Id == departmentId)
                        .Select(s => new { Id = s.Id, Name = s.Name })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }

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
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegisterStudentId,Name,Email,Department,CourseId,Date")] EnrollCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                db.EnrollCourses.Add(enrollCourse);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", enrollCourse.CourseId);
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "RegNo", enrollCourse.RegisterStudentId);
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", enrollCourse.CourseId);
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "Name", enrollCourse.RegisterStudentId);
            return View(enrollCourse);
        }

        // POST: EnrollCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegisterStudentId,Name,Email,Department,CourseId,Date")] EnrollCourse enrollCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", enrollCourse.CourseId);
            ViewBag.RegisterStudentId = new SelectList(db.RegisterStudents, "Id", "Name", enrollCourse.RegisterStudentId);
            return View(enrollCourse);
        }

        // GET: EnrollCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            if (enrollCourse == null)
            {
                return HttpNotFound();
            }
            return View(enrollCourse);
        }

        // POST: EnrollCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EnrollCourse enrollCourse = db.EnrollCourses.Find(id);
            db.EnrollCourses.Remove(enrollCourse);
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

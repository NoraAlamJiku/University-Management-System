using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class AssignToTeachersController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: AssignToTeachers
        public ActionResult Index()
        {
            var assignToTeachers = db.AssignToTeachers.Include(a => a.Course).Include(a => a.Department).Include(a => a.Teacher);
            return View(assignToTeachers.ToList());
        }

        // GET: AssignToTeachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignToTeacher assignToTeacher = db.AssignToTeachers.Find(id);
            if (assignToTeacher == null)
            {
                return HttpNotFound();
            }
            return View(assignToTeacher);
        }

        // GET: AssignToTeachers/Create
        public ActionResult Create()
        {
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name");
            return View();
        }

        // POST: AssignToTeachers/Create
        public ActionResult AssignTeacher()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }
        public ActionResult GetTeacherById(int departmentId)
        {
            return
                Json(
                    db.Teachers.Where(s => s.DepartmentId == departmentId)
                        .Select(s => new { Id = s.Id, Name = s.Name })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourseById(int departmentId)
        {
            return
                Json(
                    db.Courses.Where(s => s.DepartmentId == departmentId)
                        .Select(s => new { Id = s.Id, Code = s.Code })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetTeacherById(int departmentId)
        public ActionResult GetTeacherNameById(int departmentId)
        {
            return
                Json(
                    db.Teachers.Where(s => s.Id == departmentId)
                        .Select(s => new { Id = s.Id, CreditToBeTaken = s.CreditToBeTaken })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourseNameById(int departmentId)
        {
            return
                Json(
                    db.Courses.Where(s => s.Id == departmentId)
                        .Select(s => new { Id = s.Id, Name = s.Name, Credit = s.Credit })
                        .ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRemaingingCreditById(int departmentId)
        {
            var rept_min = (from c in db.AssignToTeachers
                            where c.TeacherId == departmentId
                            select c).Min(c => c.RemainingCredit);
            if (rept_min ==null)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            return Json(rept_min, JsonRequestBehavior.AllowGet);

        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,TeacherId,CreditToBeTaken,RemainingCredit,CourseId,Name,Credit")] AssignToTeacher assignToTeacher)
        {
            if (ModelState.IsValid)
            {
                db.AssignToTeachers.Add(assignToTeacher);
                db.SaveChanges();

               // return RedirectToAction("Create");
                if (assignToTeacher.CourseId != 0)
                {
                    var coursess = db.Courses.FirstOrDefault(x => x.Id == assignToTeacher.CourseId);
                    coursess.TeacherId = assignToTeacher.TeacherId;
                    coursess.CourseStatus = true;

                    db.Courses.AddOrUpdate(coursess);
                    db.SaveChanges();
                }
            }
           

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", assignToTeacher.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", assignToTeacher.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", assignToTeacher.TeacherId);
            return View(assignToTeacher);
        }

        public JsonResult IsTeacherExist(int TeacherId)
        {
            bool a = db.AssignToTeachers.ToList().Exists(c => c.TeacherId == TeacherId);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCourseExist(int CourseId)
        {
            bool a = db.AssignToTeachers.ToList().Exists(c => c.CourseId == CourseId);
            return Json(!a, JsonRequestBehavior.AllowGet);
        }
        // GET: AssignToTeachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignToTeacher assignToTeacher = db.AssignToTeachers.Find(id);
            if (assignToTeacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", assignToTeacher.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", assignToTeacher.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", assignToTeacher.TeacherId);
            return View(assignToTeacher);
        }

        // POST: AssignToTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,TeacherId,CreditToBeTaken,RemainingCredit,CourseId,Name,Credit")] AssignToTeacher assignToTeacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignToTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", assignToTeacher.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", assignToTeacher.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Name", assignToTeacher.TeacherId);
            return View(assignToTeacher);
        }

        // GET: AssignToTeachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignToTeacher assignToTeacher = db.AssignToTeachers.Find(id);
            if (assignToTeacher == null)
            {
                return HttpNotFound();
            }
            return View(assignToTeacher);
        }

        // POST: AssignToTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignToTeacher assignToTeacher = db.AssignToTeachers.Find(id);
            db.AssignToTeachers.Remove(assignToTeacher);
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

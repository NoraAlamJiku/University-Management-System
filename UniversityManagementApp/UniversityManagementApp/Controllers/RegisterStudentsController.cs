using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Manager;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class RegisterStudentsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: RegisterStudents
        public ActionResult Index()
        {
            var registerStudents = db.RegisterStudents.Include(r => r.Department);
            return View(registerStudents.ToList());
        }

        // GET: RegisterStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterStudent registerStudent = db.RegisterStudents.Find(id);
            if (registerStudent == null)
            {
                return HttpNotFound();
            }
            return View(registerStudent);
        }

        // GET: RegisterStudents/Create
        //private string regNo;

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            return View();
        }

        // POST: RegisterStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,ContactNo,DueDate,Address,DepartmentId,RegNo")] RegisterStudent registerStudent)
        {
            if (ModelState.IsValid)
            {
                RegisterStudentManager aManager = new RegisterStudentManager();
               // registerStudent.RegNo = aManager.GetStudentRegNo(registerStudent);
                registerStudent.RegNo = aManager.GetStudentRegNo(registerStudent);
                db.RegisterStudents.Add(registerStudent);
                db.SaveChanges();
               // return RedirectToAction("Create");
            }
            ViewBag.RegNo = "Registeration Number : " + registerStudent.RegNo;
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", registerStudent.DepartmentId);
            return View(registerStudent);
        }

        // GET: RegisterStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterStudent registerStudent = db.RegisterStudents.Find(id);
            if (registerStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", registerStudent.DepartmentId);
            return View(registerStudent);
        }

        // POST: RegisterStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,ContactNo,DueDate,Address,DepartmentId,RegNo")] RegisterStudent registerStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", registerStudent.DepartmentId);
            return View(registerStudent);
        }

        // GET: RegisterStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterStudent registerStudent = db.RegisterStudents.Find(id);
            if (registerStudent == null)
            {
                return HttpNotFound();
            }
            return View(registerStudent);
        }

        // POST: RegisterStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterStudent registerStudent = db.RegisterStudents.Find(id);
            db.RegisterStudents.Remove(registerStudent);
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

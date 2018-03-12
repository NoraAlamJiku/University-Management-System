using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Models;
using UniversityManagementApp.ViewModels;

namespace UniversityManagementApp.Controllers
{
    public class AllocateClassroomsController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: AllocateClassrooms
        //public ActionResult Index()
        //{
        //    ViewBag.departments = db.Departments.ToList();
        //    var allocateClassrooms = db.AllocateClassrooms.Include(a => a.Course).Include(a => a.DayName).Include(a => a.Department).Include(a => a.RoomNo);
        //    return View();
        //}
       // [HttpPost]
        public ActionResult Index(int departmentId = 0)
        {
            ViewBag.departments = db.Departments.ToList();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", departmentId);
            var classRoomAllocations = db.AllocateClassrooms.Include(c => c.Course).Include(c => c.Department).Include(c => c.RoomNo).ToList().FindAll(c => (c.DepartmentId == departmentId) && (c.RoomStatus == "Allocated"));
            if (departmentId != 0)
            {
                var courses = db.Courses.ToList().FindAll(c => c.DepartmentId == departmentId);
                foreach (var course in courses)
                {
                    if (!classRoomAllocations.Exists(c => c.CourseId == course.Id))
                    {
                        AllocateClassroom aClassRoomAllocation = new AllocateClassroom();
                        aClassRoomAllocation.Course = course;

                        classRoomAllocations.Add(aClassRoomAllocation);
                    }
                }
                return View(classRoomAllocations);
            }
            return View(classRoomAllocations);
        }

        public JsonResult GetStudentResultByRegNo(int departmentId)
        {
            return Json(db.AllocateClassrooms.Where(x => x.DepartmentId == departmentId).GroupBy(x => x.CourseId).Select(x => new
            {

                Id = x.FirstOrDefault().Department.Name,
                code = x.FirstOrDefault().Course.Code,
                Name = x.FirstOrDefault().Course.Name,
                Schedul = x.FirstOrDefault().RoomNo.RoomNumber + " " + x.FirstOrDefault().DayName.Name + " " + x.FirstOrDefault().StartTime + " " + x.FirstOrDefault().EndTime
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetStudentResultByRegNo(int departmentId)
        //{
        //  var result = db.AllocateClassrooms.Where(x => x.DepartmentId == departmentId).GroupBy(x => x.CourseId).ToList();
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}


        // GET: AllocateClassrooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateClassroom allocateClassroom = db.AllocateClassrooms.Find(id);
            if (allocateClassroom == null)
            {
                return HttpNotFound();
            }
            return View(allocateClassroom);
        }

        // GET: AllocateClassrooms/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code");
            ViewBag.DayNameId = new SelectList(db.DayNames, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code");
            ViewBag.RoomNoId = new SelectList(db.RoomNos, "Id", "RoomNumber");
            return View();
        }

        // POST: AllocateClassrooms/Create

        public JsonResult GetCoursesByDeptId(int deptId)
        {
            return
               Json(
                   db.Courses.Where(s => s.DepartmentId == deptId)
                       .Select(s => new { Id = s.Id, Code = s.Code })
                       .ToList(), JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetCoursesByDeptId(int deptId)
        //{
        //    var courses = db.Courses.Where(m => m.DepartmentId == deptId).ToList();
        //    return Json(courses, JsonRequestBehavior.AllowGet);
        //}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentId,CourseId,RoomNoId,DayNameId,From,To")] AllocateClassroom allocateClassroom)
        {
            if (ModelState.IsValid)
            {
               // allocateClassroom.RoomStatus = "Allocated";
                //db.AllocateClassrooms.Add(allocateClassroom);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", allocateClassroom.CourseId);
            ViewBag.DayNameId = new SelectList(db.DayNames, "Id", "Name", allocateClassroom.DayNameId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", allocateClassroom.DepartmentId);
            ViewBag.RoomNoId = new SelectList(db.RoomNos, "Id", "RoomNumber", allocateClassroom.RoomNoId);
            return View(allocateClassroom);
        }

       
        // GET: AllocateClassrooms/Edit/5
        // GET: AllocateClassrooms/Edit/5
        public JsonResult SaveRoomSchedule(AllocateClassroom allocateClassroom)
        {
            var scheduleList = db.AllocateClassrooms.Where(m => m.RoomNoId == allocateClassroom.RoomNoId && m.DayNameId == allocateClassroom.DayNameId && m.RoomStatus == "Allocated").ToList();
            if (scheduleList.Count == 0)
            {
                allocateClassroom.RoomStatus = "Allocated";

                db.AllocateClassrooms.Add(allocateClassroom);
                db.SaveChanges();
                return Json(true);
            }
            else
            {
                bool status = false;
                foreach (var allocation in scheduleList)
                {
                    if ((allocateClassroom.StartTime >= allocation.StartTime && allocateClassroom.StartTime < allocation.EndTime)
                         || (allocateClassroom.EndTime > allocation.StartTime && allocateClassroom.EndTime <= allocation.EndTime) && allocateClassroom.RoomStatus == "Allocated")
                    {
                        status = true;
                    }
                }
                if (status == false)
                {
                    allocateClassroom.RoomStatus = "Allocated";
                    db.AllocateClassrooms.Add(allocateClassroom);
                    db.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }

        }
        // GET: AllocateClassrooms/Edit/5
        // GET: AllocateClassrooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateClassroom allocateClassroom = db.AllocateClassrooms.Find(id);
            if (allocateClassroom == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", allocateClassroom.CourseId);
            ViewBag.DayNameId = new SelectList(db.DayNames, "Id", "Name", allocateClassroom.DayNameId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", allocateClassroom.DepartmentId);
            ViewBag.RoomNoId = new SelectList(db.RoomNos, "Id", "RoomNumber", allocateClassroom.RoomNoId);
            return View(allocateClassroom);
        }

        // POST: AllocateClassrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentId,CourseId,RoomNoId,DayNameId,From,To")] AllocateClassroom allocateClassroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allocateClassroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Code", allocateClassroom.CourseId);
            ViewBag.DayNameId = new SelectList(db.DayNames, "Id", "Name", allocateClassroom.DayNameId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Code", allocateClassroom.DepartmentId);
            ViewBag.RoomNoId = new SelectList(db.RoomNos, "Id", "RoomNumber", allocateClassroom.RoomNoId);
            return View(allocateClassroom);
        }

        // GET: AllocateClassrooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllocateClassroom allocateClassroom = db.AllocateClassrooms.Find(id);
            if (allocateClassroom == null)
            {
                return HttpNotFound();
            }
            return View(allocateClassroom);
        }

        // POST: AllocateClassrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllocateClassroom allocateClassroom = db.AllocateClassrooms.Find(id);
            db.AllocateClassrooms.Remove(allocateClassroom);
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

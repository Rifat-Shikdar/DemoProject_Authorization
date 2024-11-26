using DemoProject.Auth;
using DemoProject.DTOs;
using DemoProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DemoProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student

        DemoProjectEntities1 db = new DemoProjectEntities1();

        public Student Convert(StudentDTO s)
        {
            return new Student()
            {
                Name = s.FName + " " +s.LName,
                Semester = s.Semester,
                Email = s.Email,
                Year = DateTime.Now.Year,
                SId = "XX-XXXXX-X"
            };

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new StudentDTO());
        }
        [HttpPost]
        public ActionResult Create(StudentDTO s)
        {
            if (ModelState.IsValid)
            {
                var efobj = Convert(s);
                db.Students.Add(efobj);
                db.SaveChanges();
                efobj.SId = DateTime.Now.Year - 2000 + "-" + efobj.Id + "-" + efobj.Semester;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(s);
        }
        public ActionResult List()
        {
            var data = db.Students.ToList();
            return View(data);
        }

        [AdminAccess]
        public ActionResult Delete(int id)
        {
            var st = db.Students.Find(id);
            db.Students.Remove(st);
            db.SaveChanges();
            return RedirectToAction("List", "Student");
        }
    }
}
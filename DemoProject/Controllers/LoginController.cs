using DemoProject.DTOs;
using DemoProject.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DemoProjectEntities1 db = new DemoProjectEntities1();
        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginDTO());
        }
        [HttpPost]
        public ActionResult Index(LoginDTO log)
        {
            if (ModelState.IsValid)
            {
                var user = (from u in db.Users
                            where u.Username.Equals(log.Username)
                            && u.Password.Equals(log.Password)
                            select u).SingleOrDefault();
                if (user != null)
                {
                    Session["user"] = user;
                    return RedirectToAction("List", "Student");
                }


            }
            return View(log);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            User u = new FreelancerProject.User();
            ViewData["user"] = u;
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["freelancer_id"] = null;
            Session["client_id"] = null;
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var username = user.Email_id;
            var pass = user.Password;
            FreelancerDb db = new FreelancerDb();
            var u = db.Users.Where(a => a.Email_id.Equals(username) && a.Password.Equals(pass)).FirstOrDefault();
            if (db.Users.Where(a => a.Email_id.Equals(username) && a.Password.Equals(pass)).Count() == 1)
            {
                Session["client_id"] = u.Client_id;
                Session["freelancer_id"] = u.Freelancer_id;
                return Redirect("/Home/Index");
            }
            else
            {
                ViewData["errmsg"] = "Login Failed. Please Try Again";
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignUp([Bind(Include = "Email_id,Password")] User ul, [Bind(Include = "Client_name")]  Client cl, [Bind(Include = "Freelancer_name")]  Freelancer fl)
        {
            User u = new User();
            Freelancer f = new Freelancer();
            Client c = new Client();
            FreelancerDb db = new FreelancerDb();
            u.Email_id = ul.Email_id;
            u.Password = ul.Password;
            f.Freelancer_name = fl.Freelancer_name;
            c.Client_name = cl.Client_name;
            f.Rating = 0;
            c.Rating = 0;
            db.Clients.Add(c);
            db.Freelancers.Add(f);
            db.SaveChanges();

            //var f1 = db.Freelancers.OrderByDescending(a => a.Freelancer_id).FirstOrDefault();
            u.Freelancer = f;
           // var c1 = db.Clients.OrderByDescending(a => a.Client_id).FirstOrDefault();
            u.Client = c;
            db.Users.Add(u);
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}
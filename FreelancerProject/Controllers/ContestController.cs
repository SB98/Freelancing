using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerProject.Controllers
{
    public class ContestController : Controller
    {
        // GET: Contest
        public ActionResult Browse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Browse(string name, string skills, int? page)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            ViewBag.name = name;
            ViewBag.skills = skills;
            var con = from c in db.Contests
                      select c;
            int pagesize = 5;
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(name))
            {
                con = con.Where(x => x.Contest_name.Contains(name));
            }
            if (!string.IsNullOrEmpty(skills))
            {
                con = con.Where(x => x.Contest_category.Contains(skills));
            }
            con = con.OrderBy(s => s.Contest_id);
            return View(con.ToPagedList(pageNumber, pagesize));
        }
        public ActionResult Rating(int id, int id1, int r)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            //var entry = from e in db.Entries
            //          select e;
            var rating = db.Freelancers.Where(x => x.Freelancer_id == id).FirstOrDefault().Rating;
            rating = Convert.ToInt16((rating + r) / 2);
            db.Freelancers.Where(x => x.Freelancer_id == id).FirstOrDefault().Rating = rating;
            db.Entries.Where(x => x.Entry_id == id).FirstOrDefault().Rating = r;
            db.SaveChanges();
            return View("Show", new { id = id });
        }
        public ActionResult MyContests(int? page)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            int c_id = Convert.ToInt32(Session["client_id"]);

            int pagesize = 5;
            int pageNumber = (page ?? 1);

            //var clients = from c in db.Clients
            //            select c;
            var con = (db.Contests.Where(x => x.Client_id == c_id));

            con = con.OrderBy(s => s.Contest_id);
            return View(con.ToPagedList(pageNumber, pagesize));

        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contest c)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            if (Session["client_id"] != null)
            {
                FreelancerDb db = new FreelancerDb();
                Contest co = new Contest();
                co.Contest_category = c.Contest_category;
                co.Contest_name = c.Contest_name;
                co.Contest_desc = c.Contest_desc;
                co.Prize = c.Prize;
                co.Duration = c.Duration;
                co.Contest_status = "Open";
                co.Contest_date = DateTime.Today;
                int id = Convert.ToInt32(Session["client_id"]);
                var ob = db.Clients.Find(id);
                co.Client = ob;
                db.Contests.Add(co);
                db.SaveChanges();
                return Redirect("/Contest/MyContests");
            }
            else
            {
                return Redirect("/User/Login");
            }
        }

        public ActionResult Show(int id)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            int user_id = Convert.ToInt32(Session["client_id"]);

            Contest c = db.Contests.Find(id);
            if (c.Contest_status.Equals("Open"))
            {
                var day = c.Contest_date.ToString().Split('-')[0];
                var total = Convert.ToInt32(day) + c.Duration;
                var c_d = DateTime.Today.ToString().Split(' ')[0];
                var final = c_d.Split('-')[0];
                var ans = Convert.ToInt32(total) - Convert.ToInt32(final);
                if (ans == 0)
                {
                    ViewData["data"] = "Open Today";
                }
                else if (ans > 0)
                {
                    ViewData["data"] = "Open" + ans + "Remaining";
                }
                else
                {
                    ViewData["data"] = "Pending";
                }
            }

            else
            {
                ViewData["data"] = c.Contest_status;
            }
            ViewData["contest"] = c;

            List<Entry> entries = db.Entries.ToList();
            
            List<Entry> contestEntries = entries.Where(e => e.Contest_id == id).OrderByDescending(x => x.Entry_id).OrderByDescending(x => x.Rating).OrderByDescending(x => x.Entry_status).ToList();     
            return View(contestEntries);
        }

        public ActionResult Entry_Action(int id, string perform, int cid)

        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();

            List<Entry> entries = db.Entries.ToList();
            var entry = entries.Find(e => e.Entry_id == id);
            if (perform.Equals("Withdraw"))
            {
                entry.Entry_status = "Withdrawn";
            }
            if (perform.Equals("Reject"))
            {
                entry.Entry_status = "Rejected";
            }
            if (perform.Equals("Winner"))
            {
                entry.Entry_status = "Winner";
                var contest = db.Contests.Find(cid);
                contest.Contest_status = "Awarded";
                var e = db.Entries.Find(id);
                contest.Entry = e;
            }
             
            db.SaveChanges();
            return Redirect("/Contest/Show/"+cid);
        }

        public ActionResult Handover(int id)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            var contest = db.Contests.Find(id);
            return View(contest);
        }

        [HttpPost]
        public ActionResult Handover(Contest c,HttpPostedFileBase imgFile)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            var contest = db.Contests.Find(c.Contest_id);
            contest.Contest_status = "Closed";
            db.SaveChanges();
            string fileName = Path.GetFileName(imgFile.FileName);
            string path;
            string extention = fileName.Split('.')[1];
            string myFile = Convert.ToInt32(Session["freelancer_id"]) + ((DateTime.Today.ToString()).Split(' ')[0]).Replace("-", "") + ((DateTime.Today.ToString()).Split(' ')[1]).Replace(":", "") + extention;

           

            path = Path.Combine(Server.MapPath("~/Files/"), myFile);

            imgFile.SaveAs(path);
            return Redirect("/Contest/Browse/");
        }

        public ActionResult Payment(int id)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            var contest = db.Contests.Find(id);
            if (contest.Winner_id != null)
            {
                var winner = db.Entries.Find(contest.Winner_id).Freelancer;
                ViewData["winner"] = winner;
            }
           
            return View(contest);
        }

        [HttpPost]
        public ActionResult Payment(Contest contest)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null)
            {
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            Payment payment = new Payment();
            payment.Contest = contest;
            payment.Payment_amount = contest.Prize;
            payment.Payment_date = DateTime.Today;
            payment.Freelancer = db.Entries.Find(contest.Winner_id).Freelancer;
            db.Payments.Add(payment);
            db.SaveChanges();
            return Redirect("/Home/Index/");
        }
    }
}
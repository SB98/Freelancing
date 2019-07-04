using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancerProject.Controllers
{
    public class EntryController : Controller
    {
        // GET: Entry
        public ActionResult Upload(int id)
        {
            if(Session["freelancer_id"]==null || Session["client_id"] == null){
                return Redirect("/User/Login");
            }
            FreelancerDb db = new FreelancerDb();
            Entry entry = new Entry();
            ViewData["Entry"] = entry;
            var contest = db.Contests.Find(id);
            
            return View(contest);
        }

        [HttpPost]
        public ActionResult Upload(Contest contest,Entry entry,HttpPostedFileBase imgFile)
        {
            if (Session["freelancer_id"] == null || Session["client_id"] == null){
                return Redirect("/User/Login");
            }
            FreelancerDb context = new FreelancerDb();
            Entry e = new Entry();
           
            e.Entry_header = entry.Entry_header;
            e.Entry_status = "Open";
            e.Entry_desc = entry.Entry_desc;
            e.Entry_date = DateTime.Today;
            e.Rating = 0;


            string fileName = Path.GetFileName(imgFile.FileName);
            string path;
            string extention = fileName.Split('.')[1];
            string myFile = "1" + ((DateTime.Today.ToString()).Split(' ')[0]).Replace("-","") + ((DateTime.Today.ToString()).Split(' ')[1]).Replace(":", "") + extention;

            e.ImageFile = myFile;

            path = Path.Combine(Server.MapPath("~/Images/"), myFile);
            e.Contest = contest;
            var freelancer = context.Freelancers.Find(Convert.ToInt32(Session["freelancer_id"]));
            e.Freelancer = freelancer;
            imgFile.SaveAs(path);

            context.Entries.Add(e);

            if (context.SaveChanges()>0){
                return Redirect("/Contest/Show/"+contest.Contest_id);
            }
            else
            {
                return View();
            }
            
        }
    }
}
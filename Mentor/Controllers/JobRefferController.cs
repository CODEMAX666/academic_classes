using Mentor.BE;
using Mentor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Mentor.DAL.JobRefferDAL;

namespace Mentor.Controllers
{
    public class JobRefferController : Controller
    {
        public ActionResult rough()
        {
            return View();
        }
        // GET: JobReffer
        public ActionResult JobReffer()
        {
            return View();
        }
        public ActionResult JobsRequested()
        {
            return View();
        }
        public ActionResult RefferJob(
         string StatusID,
         string jobID,
         string ViewerID,
         string memberID
          )
        {

            ProfileBE reffer = new ProfileBE();
            //addJob.JobID = Convert.ToInt32(jobID);
            reffer.MemberID = Convert.ToInt32(memberID);
            reffer.ViewerID = Convert.ToInt32(ViewerID);
            reffer.JobID = Convert.ToInt32(jobID);
            reffer.StatusID = Convert.ToInt32(StatusID);
            //reffer.MenteeJobrefID = Convert.ToInt32(MenteeJobRefID);

            new JobRefferDAL().JobReffer(reffer);

            return RedirectToAction("JobsList", "Job");
        }
        //jobrefstatusupdate
        public ActionResult UpdRefferJob(
        string StatusID,
        string jobID,
        string ViewerID,
        string memberID
         )
        {

            ProfileBE reffer = new ProfileBE();
            //addJob.JobID = Convert.ToInt32(jobID);
            reffer.MemberID = Convert.ToInt32(memberID);
            reffer.ViewerID = Convert.ToInt32(ViewerID);
            reffer.JobID = Convert.ToInt32(jobID);
            reffer.StatusID = Convert.ToInt32(StatusID);
            //reffer.MenteeJobrefID = Convert.ToInt32(MenteeJobRefID);

            new JobRefferDAL().StatusUpd(reffer);

            return RedirectToAction("JobsList", "Job");
        }
        //mentor reff list
        public JsonResult GetJobrefList()
        {
            JobRefferListBE jobrefList = new JobRefferListBE();
            jobrefList.JobReffers = new JobRefferDAL().MyJobrefferLists(19);


            return Json(jobrefList.JobReffers, JsonRequestBehavior.AllowGet);
        }
        //mentee job ref request list
        public JsonResult GetJobreqList()
        {
            JobRequestListBE jobreqList = new JobRequestListBE();
            jobreqList.JobReffers = new JobRefferDAL().MyJobrequestList(19);


            return Json(jobreqList.JobReffers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectedExpLists()
        {
            add_JobBE addJob = new add_JobBE();
            addJob.CareerLevelList = new AddStatusDropDown().GetStatusLists();

            return Json(addJob.CareerLevelList, JsonRequestBehavior.AllowGet);
        }
    }
  
}
using Mentor.BE;
using Mentor.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{   
    public class JobController : Controller
    {
        public ActionResult prac()
        {
            return View();
        }
        // GET: Job
        public ActionResult AddJobs()
        {
            return View();
        }
        public ActionResult JobsList()
        {       
            return View();
        }
        // view job
        public ActionResult JobDetails(String memberId, int JobId)
        {
            AddJobBE JobDet = new AddJobBE();
            JobListDAL JobDAL = new JobListDAL();
            JobDet = JobDAL.GetJobBE(JobId);
            return View(JobDet);
        }
        public ActionResult EditJob(String memberId, int JobId)
        {
            AddJobBE editJob = new AddJobBE();
            JobListDAL JobDAL = new JobListDAL();
            editJob = JobDAL.GetJobBE(JobId);
            // string date = editJob.AppClosingDate;

            //string[] mydate = date.Split('-');
            // date = mydate[0];
            // IFormatProvider culture = new CultureInfo("en-US", true);
            // //string DateString = null;
            // DateTime dateVal = DateTime.ParseExact(mydate[0], "yyyy-MM-dd", culture);
            // //split string and store it into array
            // editJob.AppClosingDate = "date";
            return View(editJob);
        }
        //Adding Job Controller
        public ActionResult Add_Job(
          string jobID,
          string memberID,
          string car,
          string dom,
          string cat,
          string subC,
          string Title,
          string Comp,
          string C_Email,
          string city,
          string Exp,
          string Date,
          string Desc,
          int Is_Pub)
        {
            
            AddJobBE addJob = new AddJobBE();
            //addJob.JobID = Convert.ToInt32(jobID);
            addJob.MemberId = Convert.ToInt32(memberID);
            addJob.MemberCareerLevel = Convert.ToInt32(car);
            addJob.MemberDomain = Convert.ToInt32(dom);
            addJob.Category = Convert.ToInt32(cat);
            addJob.SubCategory = Convert.ToInt32(subC);
            addJob.Title = Title;
            addJob.Company = Comp;
            addJob.City = city;
            addJob.RequiredExperience = Convert.ToInt32(Exp);
            addJob.AppClosingDate = Date;
            addJob.JobKPIS = Desc;
            addJob.ComEmail = C_Email;
            addJob.IsPublic = Is_Pub;
           
            new AddJobDAL().addjob(addJob);

            return RedirectToAction("JobsList","Job");
        }

        //Experience Dropdown Controller
        public JsonResult GetSelectedExpLists()
        {
            add_JobBE addJob = new add_JobBE();
            addJob.CareerLevelList = new AddExpDropDown().GetSelectedExpLists();

            return Json(addJob.CareerLevelList, JsonRequestBehavior.AllowGet);
        }

        //Job List Controller
        public JsonResult GetJobList()
        {
            JobListBE jobList = new JobListBE();
            jobList.jobLists = new JobListDAL().myJobLists(19);


            return Json(jobList.jobLists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetJobListByMemberId(int MemberID ,  int menteeid)
        {
            JobListBE jobList = new JobListBE();
            jobList.jobLists = new JobListDAL().joblistbyID(MemberID , menteeid);


            return Json(jobList.jobLists, JsonRequestBehavior.AllowGet);
        }

        //Update Job Controller
        public ActionResult Update_Job(
          string jobID,
          string memberID,
          string car,
          string dom,
          string cat,
          string subC,
          string Title,
          string Comp,
          string C_Email,
          string city,
          string Exp,
          string Date,
          string Desc,
          int Is_Pub)
        {
            AddJobBE addJob = new AddJobBE();
            addJob.JobID = Convert.ToInt32(jobID);
            addJob.MemberId = Convert.ToInt32(memberID);
            addJob.MemberCareerLevel = Convert.ToInt32(car);
            addJob.MemberDomain = Convert.ToInt32(dom);
            addJob.Category = Convert.ToInt32(cat);
            addJob.SubCategory = Convert.ToInt32(subC);
            addJob.Title = Title;
            addJob.Company = Comp;
            addJob.City = city;
            addJob.RequiredExperience = Convert.ToInt32(Exp);
            addJob.AppClosingDate = Date;
            addJob.JobKPIS = Desc;
            addJob.ComEmail = C_Email;
            addJob.IsPublic = Convert.ToInt32(Is_Pub);
            new AddJobDAL().Updatejob(addJob);

            return RedirectToAction("JobsList","Job");
        }
        //Deleting Job Controller
        public ActionResult Delete(string memberId, string JobId)
        {
            JobListDAL jobList = new JobListDAL();
            jobList.Deletejob(Convert.ToInt32(memberId),Convert.ToInt32(JobId));

            return RedirectToAction("JobsList","Job");
        }
    }
}
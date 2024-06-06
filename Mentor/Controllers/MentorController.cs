using Mentor.BE;
using Mentor.DAL;
using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using MentorEF.Model;

namespace Mentor.Controllers
{
    public class MentorController : Controller
    {
        // GET: Mentor
        public ActionResult ScheduleTimings()
        {
            /* var dt = new DataTable();
             dt.Columns.Add("StartTime",typeof(TimeSpan));
             dt.Columns.Add("EndTime", typeof(TimeSpan));
             dt.Columns.Add("[Duration]", typeof(int));*/

            // dt.Columns.Add
            return View();
        }

        public void InsertScheduleTimings(string day, string sdd1, string edd1, string d1, string sdd2, string edd2, string d2, string sdd3, string edd3, string d3)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            new ScheduleTimingsDAL().Insert_Schedule_Timing(ID, day, sdd1, edd1, d1, sdd2, edd2, d2, sdd3, edd3, d3);
        }

        public void UpdateScheduleTiming(int mmsid, string day, string sdd1, string edd1, string d1)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            new ScheduleTimingsDAL().Update_Schedule_Timing(mmsid, ID, day, sdd1, edd1, d1);
        }

        public void DeleteScheduleTiming(int mmsid)
        {

            new ScheduleTimingsDAL().Delete_Schedule_Timing(mmsid);
        }


        public JsonResult GetSlotList()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            ScheduleTimingsBE st = new ScheduleTimingsBE();
            st.ScheduleTimingList = new ScheduleTimingsDAL().NumberofSlots(ID);
            return Json(st.ScheduleTimingList, JsonRequestBehavior.AllowGet);
        }
        //****Mentee List ****//
        public ActionResult MenteeList(int? page)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                int pageSize = 0, pageindex = 1;
                double noOfItemsOnPage = 9;
                pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

                List<MenteeListBE> getMentees = new List<MenteeListBE>();
                //List<SelectedList> getList = new List<SelectedList>();
                //getList = new MenteeListDAL().getStatusList();
                int ID = Convert.ToInt32(cookie.Values["ID"]);
                getMentees = new MenteeListDAL().Menteelist(ID);

                ViewBag.List = getMentees;  //List of mentees

                pageSize = (int)Math.Ceiling(getMentees.Count() / noOfItemsOnPage);
                IPagedList<MenteeListBE> stu = (IPagedList<MenteeListBE>)getMentees.ToPagedList(pageindex, Convert.ToInt32(noOfItemsOnPage));         //Paging List
                return View(stu);
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }
        public ActionResult DeleteMentee(int id)
        {
            int MenteeID = id;

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MentorID = 0;
            if (cookie != null)
            {
                MentorID = Convert.ToInt32(cookie.Values["ID"]);
            }

            bool flag = new MenteeListDAL().DeleteMentee(MenteeID, MentorID);
            return RedirectToAction("Menteelist");
        }

        public ActionResult AcceptRequest(int id)
        {
            int MenteeID = id;

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MentorID = 0;
            if (cookie != null)
            {
                MentorID = Convert.ToInt32(cookie.Values["ID"]);
            }

            bool flag = new MenteeListDAL().AcceptReqOfMentee(MenteeID, MentorID);
            return RedirectToAction("Menteelist");
        }

        public ActionResult RejectRequest(int id)
        {
            int MenteeID = id;

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MentorID = 0;
            if (cookie != null)
            {
                MentorID = Convert.ToInt32(cookie.Values["ID"]);
            }

            bool flag = new MenteeListDAL().RejectReqOfMentee(MenteeID, MentorID);
            return RedirectToAction("Menteelist");
        }

        public ActionResult MenteeProfile()
        {
            return View();
        }
        public ActionResult TaskManagement()
        {
            return View();
        }

        public ActionResult JobReferal()
        {
            return View();
        }

        public ActionResult PackageManagement()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                int MentorId = Convert.ToInt32(cookie.Values["ID"]);
                MentorPackageBE package = new MentorPackageBE();
                package.MentorPackageList = new MentorPackageDAL().GET_MentorOwnedPackage(MentorId);
                package.MemberMenteeCareerLevelList = new MentorPackageDAL().Get_MenteeCareerLevelList(MentorId);
                package.MenteePackageNameList = new MentorPackageDAL().GET_MenteePackageList(Convert.ToInt32(package.MemberMenteeCareerLevel));
                Session["MemberMenteeCareerLevelList"] = package.MemberMenteeCareerLevelList;
                foreach (var item in package.MentorPackageList)
                {
                    item.MenteePackageNameList = new MentorPackageDAL().GET_MenteePackageList(Convert.ToInt32(item.MemberMenteeCareerLevel));
                }
                return View(package);
            }
            else
            {
                return RedirectToAction("Login", "Member");
            }
        }
        public JsonResult GetCareerLevelId(int Id)
        {
            return Json(new MentorPackageDAL().GET_MenteePackageList(Id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPackageId(int Id, int careerId)
        {
            string description = "";
            if (Id > 0)
            {
                string value = Id.ToString();
                List<PackageList> list = new MentorPackageDAL().GET_MenteePackageList(careerId);
                foreach (var item in list)
                {
                    if (item.Value == value)
                    {
                        description = item.MenteePackageDescription;
                    }
                }
                return Json(description, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(description, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult PackageManagement(MentorPackageBE packages)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MentorId = Convert.ToInt32(cookie.Values["ID"]);
            new MentorPackageDAL().Insert_Update_MentorPackage(packages, MentorId);

            return RedirectToAction("PackageManagement");
        }
        public JsonResult CareerLevelListDropdown()
        {
            List<SelectedList> list = new List<SelectedList>();
            list = (List<SelectedList>)Session["MemberMenteeCareerLevelList"];
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddTraining(string TrainingId = null)

        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                int MentorId = Convert.ToInt32(cookie.Values["ID"]);
                MentorTraining training = new MentorTraining();
                training = new MentorTrainingDAL().GetTraining(Convert.ToInt32(TrainingId));
                training.CareerLevelList = new MemberDAL().GetCareerLevelList();
                training.ExperienceList = new MentorTrainingDAL().GetSelectedExpList();
                training.DurationList = new MentorTrainingDAL().GetTrainingDurationList();
                training.MentorId = MentorId;
                training.DomainList = new MemberDAL().GetDomainList(Convert.ToInt32(training.CareerLevel));
                training.CategoryList = new MemberExperienceDAL().GetCategoryList(Convert.ToInt32(training.Domain));
                training.SubCategoryList = new MemberExperienceDAL().GetSubCategoryList(Convert.ToInt32(training.Category));
                return View(training);
            }
            else
            {
                return RedirectToAction("Login", "Member");
            }

        }
        public JsonResult GetDomainId(int Id)
        {
            return Json(new MemberExperienceDAL().GetCategoryList(Id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddTraining(MentorTraining training)
        {
            string temp = Request.Form["Experience"].ToString();
            //training.Experience = temp;
            new MentorTrainingDAL().Training_Insert_Update(training);
            string url = string.Format("/Mentor/AddTrainingSchedule?trainingId={0}", training.MentorTrainingId);
            //TempData["trainingid"] = training.MentorTrainingId;
            //TempData["mydata"] = training;
            //return new RedirectResult(@"~\Mentor\AddTrainingSchedule\");
            return Redirect(url);
        }

        public ActionResult AddTrainingSchedule(string trainingId)
        {
            //HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            //int MentorId = Convert.ToInt32(cookie.Values["ID"]);
            MentorTraining training = new MentorTraining();
            if (Convert.ToInt32(trainingId) > 0)
            {
                training = new MentorTrainingDAL().GetTraining(Convert.ToInt32(trainingId));
            }
            else
            {
                training = new MentorTrainingDAL().GetLastTrainingEntry();
            }
            TempData["mydata"] = training;
            ViewBag.TrainingId = training.MentorTrainingId;

            return View();
        }
        [HttpPost]
        public ActionResult AddTrainingSchedule(List<SelectedSlotsList> TrainingSchedule)
        {
            if (TrainingSchedule.Count > 0)
            {
                MentorTraining training = TempData["mydata"] as MentorTraining;
                new MentorTrainingDAL().Insert_Update_TrainingSchedule(TrainingSchedule, training.MentorId, training.MentorTrainingId);
                return RedirectToAction("TrainingList");
            }
            else
            {
                return RedirectToAction("AddTrainingSchedule");
            }

        }
        public ActionResult DeleteTrainingSlot(string TrainingId, string ScheduleId, string SlotId)
        {
            new MentorTrainingDAL().DeleteTrainingSlot(TrainingId, ScheduleId, SlotId);
            string url = string.Format("/Mentor/AddTrainingSchedule?trainingId={0}", TrainingId);
            return Redirect(url);
        }
        public ActionResult TrainingList()
        {
            return View();
        }
        public JsonResult GetTrainingList(int memberid)
        {
            //HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            //int MentorId = Convert.ToInt32(cookie.Values["ID"]);
            MentorTraining training = new MentorTraining();
            training.TrainingList = new MentorTrainingDAL().GetTrainingList(memberid);
            foreach (var item in training.TrainingList)
            {
                item.StartDate = item.TrainingStartDate.ToShortDateString();
            }
            return Json(training.TrainingList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTraining(string TrainingId, string MentorId)
        {
            new MentorTrainingDAL().DeleteTraining(TrainingId, MentorId);
            return RedirectToAction("TrainingList");
        }
        public ActionResult ViewTraining(string TrainingId)
        {
            ViewBag.Trainingid = TrainingId;
            MentorTraining training = new MentorTrainingDAL().GetTrainingView(TrainingId);
            return View(training);
        }
        public JsonResult GetTrainingSchedule(string TrainingId, string MentorId)
        {
            List<SelectedSlotsList> trainingSchedule = new MentorTrainingDAL().GetTrainingSchedule(TrainingId, MentorId);
            return Json(trainingSchedule, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllSlots(string memberid)
        {
            ScheduleTimingsBE allslots = new ScheduleTimingsBE();
            allslots.SlotsList = new MentorTrainingDAL().GetAllSlots(Convert.ToInt32(memberid));
            return Json(allslots.SlotsList, JsonRequestBehavior.AllowGet);
        }
    }
}
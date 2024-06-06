using Mentor.BE;
using Mentor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
        //***** Populate Ticker Table Jobs on the Basis of Applied Filters****//
        public JsonResult GetFilteredJobList(int mcareerid, int mdomianid, int mcategoryid, int msubcategoryid)
        {
            return Json(new JobListDAL().FilteredJobList(mcareerid, mdomianid, mcategoryid, msubcategoryid), JsonRequestBehavior.AllowGet);
        }
        //****Populate SubCategories Name Index Page****//
        public JsonResult GetSubCategoryNames()
        {
            string array = new DropDownPopulate().GetSubCategoryNames();
            string[] arr = array.Split(',').ToArray();
            return Json(arr, JsonRequestBehavior.AllowGet);
        }
        //****Populate Index Page Ticker Table Training on the Basis of Applied Filters****//
        public JsonResult GetFilteredTrainingList(int mcareerid, int mdomianid, int mcategoryid, int msubcategoryid)
        {
            List<MentorTraining> list = new List<MentorTraining>();
            list = new MentorTrainingDAL().FilteredTrainingList_Index(mcareerid, mdomianid, mcategoryid, msubcategoryid);
            foreach (var item in list)
            {
                string date = item.StartDate;
                var array = date.Split(' ');
                var newdate = array[0];
                item.StartDate = newdate;
            }
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        //*****Populate Training Title dropdown on Index Page****//
        public JsonResult GetTrainingTitleList()
        {
            return Json(new DropDownPopulate().GetTrainingTitleList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
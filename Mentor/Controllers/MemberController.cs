using Mentor.BE;
using Mentor.DAL;
using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Login()
        {
            //return RedirectToAction("MemberProfileSetting");
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            //byte[] encData_byte2 = new byte[password.Length];
            //encData_byte2 = System.Text.Encoding.UTF8.GetBytes(password);
            //string encodedData = Convert.ToBase64String(encData_byte2);
            int MemberId = new LoginDAL().CheckLogin(email, password);
            if (MemberId > 0)
            {
                MemberBE member = new MemberBE();
                member.Email = email;
                member.Password = password;
                member.MemberId = MemberId;
                new GenericFunctions().SetCookie(member);
                //int MemberId = new LoginDAL().GetId(email);
                member = new MemberDAL().GetMemberById(MemberId);
                if (member.IsFirstTimeLogin == true)
                {
                    return RedirectToAction("MemberProfileSetting");

                }
                else
                {
                    return RedirectToAction("Dashboard");
                }

            }
            else if (MemberId == -11)
            {
                ViewBag.Validation = "False";
                return View();
            }
            else
            {
                ViewBag.Validation = "False";
                return View();

            }
        }

        public ActionResult Logout()
        {

            new GenericFunctions().ExpireCookieUserLogin();
            return RedirectToAction("Login");
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult MemberProfileSetting()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                int ID = Convert.ToInt32(cookie.Values["ID"]);
                MemberBE member = new MemberDAL().GetMemberById(ID);
                if (member.MemberId != 0)
                {
                    member.MemberLevelList = new MemberDAL().GetCareerLevelList();
                    //member.MemberPossibleMentee = String.Join(",", member.MemberPossibleMentee);
                    /*if (!string.IsNullOrEmpty(member.MemberDomain))
                    {
                        member.MemberDomainArray = member.MemberDomain.Split(',').ToArray();
                    }*/

                    if (!string.IsNullOrEmpty(member.MemberPossibleMentee))
                    {
                        member.MemberPossibleMenteeArray = member.MemberPossibleMentee.Split(',').ToArray();
                    }
                    //member.MemberPossibleMenteeArray = member.MemberPossibleMentee.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                    member.GenderList = new MemberDAL().GetGenderList();
                    member.MemberDomainList = new MemberDAL().GetDomainList(Convert.ToInt32(member.MemberLevelID));
                    member.CareerLevelForMenteeSelectionList = new MemberDAL().GetCareerLevelForMenteeList();
                    return View(member);
                }
                else
                {
                    ViewBag.Validation = "False";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public JsonResult GetCareerLevelId(int Id)
        {
            return Json(new MemberDAL().GetDomainList(Id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult MemberProfileSetting(MemberBE member)
        {
            if (member.PhotoURL == null && member.PhotoFile != null)
            {
                //var getSize = member.PhotoFile.ContentLength / 1024;
                string extension = Path.GetExtension(member.PhotoFile.FileName);
                string filename = member.MemberId + extension;
                member.PhotoURL = "/Images/MemberProfilePhotos/" + filename;
                filename = Path.Combine(Server.MapPath("/Images/MemberProfilePhotos/"), filename);
                member.PhotoFile.SaveAs(filename);
            }
            string Domain = Request.Form["MemberDomainID"].ToString();
            member.MemberLevelList = new MemberDAL().GetCareerLevelList();
            member.MemberDomainList = new MemberDAL().GetDomainList(Convert.ToInt32(member.MemberLevelID));
            member.CareerLevelForMenteeSelectionList = new MemberDAL().GetCareerLevelForMenteeList();

            //---------------------------------------------------------------------------
            if (member.MemberLevelID != 1)
            {
                if (Request.Form["MemberPossibleMenteeArray"] != null)
                {
                    string MemberPossibleMentee = Request.Form["MemberPossibleMenteeArray"].ToString();
                    if (!string.IsNullOrEmpty(MemberPossibleMentee))
                    {
                        string[] MemberMenteeList = MemberPossibleMentee.Split(',');
                        member.MemberMenteeCareerLevelList = new List<MemberMenteeCareerLevel>();
                        foreach (var id in MemberMenteeList)
                        {
                            MemberMenteeCareerLevel memberMentee = new MemberMenteeCareerLevel();
                            memberMentee.MemberCareerLevelId_fk = Convert.ToInt32(id);
                            member.MemberMenteeCareerLevelList.Add(memberMentee);
                        }

                    }
                }


            }
            member.Gender = Request.Form["Gender"].ToString();
            //string Domain = member.MemberDomain;
            /* if (!string.IsNullOrEmpty(Domain))
             {
                 string[] MemberDomain = Domain.Split(',');
                 member.MemberSelectedDomainList = new List<MemberSelectedDomain>();
                 foreach (var id in MemberDomain)
                 {
                     MemberSelectedDomain selected = new MemberSelectedDomain();
                     selected.MemberDomainID = Convert.ToInt32(id);
                     member.MemberSelectedDomainList.Add(selected);
                 }

             }*/
            new MemberDAL().UpdateMember(member);
            return RedirectToAction("MemberEducation");

        }
        public ActionResult MemberEducation()
        {

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                MemberEducation education = new MemberEducation();
                int MemberId = Convert.ToInt32(cookie.Values["ID"]);
                education.MemberEducationList = new MemberEducationDAL().GetMemberEducation(MemberId);
                education.DegreeLevelList = new MemberEducationDAL().GetDegreeLevelList();
                Session["DegreeLevelList"] = education.DegreeLevelList;
                foreach (var level in education.MemberEducationList)
                {
                    level.DegreeTitleList = new MemberEducationDAL().GetDegreeTitleList(Convert.ToInt32(level.DegreeLevelName));
                }
                //Session["DegreeTitleList"] = education.DegreeTitleList;
                return View(education);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public JsonResult GetDegreeLevelId(int Id)
        {
            List<SelectedList> list = new List<SelectedList>();
            list = new MemberEducationDAL().GetDegreeTitleList(Id);
            Session["DegreeTitleList"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult MemberEducation(List<MemberEducation> education)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MemberId = Convert.ToInt32(cookie.Values["ID"]);
            new MemberEducationDAL().Insert_Update_MemberEducation(education, MemberId);
            return RedirectToAction("MemberExperience");
        }

        public JsonResult DegreeLevelListDropdown()
        {
            List<SelectedList> list = new List<SelectedList>();
            list = (List<SelectedList>)Session["DegreeLevelList"];
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DegreeTitleListDropdown()
        {
            List<SelectedList> list = new List<SelectedList>();
            list = (List<SelectedList>)Session["DegreeTitleList"];
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MemberExperience()
        {

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                MemberExperience experience = new MemberExperience();
                int MemberId = Convert.ToInt32(cookie.Values["ID"]);
                experience.MemberExperienceList = new MemberExperienceDAL().GetMemberExperience(MemberId);
                experience.MemberCategoryList = new MemberExperienceDAL().GetCategoryList(1);
                Session["MemberCategoryList"] = experience.MemberCategoryList;
                foreach (var level in experience.MemberExperienceList)
                {
                    level.MemberSubCategoryArray = level.MemberSubCategory.Split(',').ToArray();
                    level.MemberSubCategoryList = new MemberExperienceDAL().GetSubCategoryList(Convert.ToInt32(level.MemberCategory));
                }
                //Session["MemberSubCategoryList"] = experience.MemberSubCategoryList;

                return View(experience);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public JsonResult GetCategoryId(int Id)
        {
            List<SelectedList> list = new List<SelectedList>();
            Session["MemberSubCategoryList"] = list = new MemberExperienceDAL().GetSubCategoryList(Id);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult MemberExperience(MemberExperience experience)
        {
            if (!string.IsNullOrEmpty(experience.MemberSubCategory))
            {
                string[] SubCategoryList = experience.MemberSubCategory.Split(',');
                experience.SubCategoryList = new List<Category>();
                foreach (var id in SubCategoryList)
                {
                    Category selected = new Category();
                    selected.CategoryId = Convert.ToInt32(experience.MemberCategory);
                    selected.SubCategoryId = Convert.ToInt32(id);
                    experience.SubCategoryList.Add(selected);
                }

            }
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MemberId = Convert.ToInt32(cookie.Values["ID"]);
            new MemberExperienceDAL().Insert_Update_MemberExperience(experience, MemberId);
            //experience.MemberCategoryList = new MemberExperienceDAL().GetCategoryList(1);
            //experience.MemberSubCategoryList = new MemberExperienceDAL().GetSubCategoryList(1);
            return RedirectToAction("MemberInterest");
        }

        public JsonResult CategoryListDropdown()
        {
            List<SelectedList> list = new List<SelectedList>();
            list = (List<SelectedList>)Session["MemberCategoryList"];
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubCategoryListDropdown()
        {
            List<SelectedList> list = new List<SelectedList>();
            list = (List<SelectedList>)Session["MemberSubCategoryList"];
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MemberInterest()
        {

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];

            if (cookie != null)
            {
                MemberInterest interest = new MemberInterest();
                int MemberId = Convert.ToInt32(cookie.Values["ID"]);
                interest.MemberInterestList = new MemberInterestDAL().GetMemberInterest(MemberId);
                interest.MemberCategoryList = new MemberExperienceDAL().GetCategoryList(1);
                interest.MemberSubCategoryList = new MemberExperienceDAL().GetSubCategoryList(Convert.ToInt32(interest.MemberCategory));
                Session["MemberCategoryList"] = interest.MemberCategoryList;

                foreach (var level in interest.MemberInterestList)
                {
                    level.MemberSubCategoryList = new MemberExperienceDAL().GetSubCategoryList(Convert.ToInt32(level.MemberCategory));
                }
                return View(interest);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult MemberInterest(List<MemberInterest> interest)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MemberId = Convert.ToInt32(cookie.Values["ID"]);
            new MemberInterestDAL().Insert_Update_MemberInterest(interest, MemberId);
            return RedirectToAction("MemberInterest");
        }

        public ActionResult ShowProfile(string memberid, string viewerid)
        {
            if (memberid == null && viewerid == null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
                string MemberId = cookie.Values["ID"].ToString();
                viewerid = MemberId;
                memberid = MemberId;
            }
            ViewBag.memberid = memberid;
            ViewBag.Viewerid = viewerid;

            return View();
        }

        public JsonResult GetDetails(string memberid, string viewerid)
        {
            ProfileBE profile = new ProfileBE();
            profile.ProfileList = new ProfileDAL().Profile(Convert.ToInt32(memberid), Convert.ToInt32(viewerid));
            return Json(profile.ProfileList[0], JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEducation()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            MemberEducation profile = new MemberEducation();
            profile.MemberEducationList = new MemberEducationDAL().GetMemberEducation(ID);
            return Json(profile.MemberEducationList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEducationByMemID(int memberID)
        {
            MemberEducation profile = new MemberEducation();
            profile.MemberEducationList = new MemberEducationDAL().GetMemberEducation(memberID);
            return Json(profile.MemberEducationList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTrainingsByMemID(int mentorID, int menteeid)
        {
            TrainingsListBE trainings = new TrainingsListBE();
            trainings.myTrainings = new ProfileDAL().TraininglistbyID(mentorID, menteeid);
            return Json(trainings.myTrainings, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetExperience()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            MemberExperience profile = new MemberExperience();
            profile.MemberExperienceList = new MemberExperienceDAL().GetMemberExperience(ID);
            return Json(profile.MemberExperienceList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetExperienceByMemId(int memberid)
        {
            //HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            //int ID = Convert.ToInt32(cookie.Values["ID"]);
            MemberExperience profile = new MemberExperience();
            profile.MemberExperienceList = new MemberExperienceDAL().GetMemberExperience(memberid);
            return Json(profile.MemberExperienceList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInterest()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            MemberInterest profile = new BE.MemberInterest();
            profile.MemberInterestList = new MemberInterestDAL().GetMemberInterest(ID);
            return Json(profile.MemberInterestList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInterestByMemId(int memberid)
        {
            //HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            //int ID = Convert.ToInt32(cookie.Values["ID"]);
            MemberInterest profile = new BE.MemberInterest();
            profile.MemberInterestList = new MemberInterestDAL().GetMemberInterest(memberid);
            return Json(profile.MemberInterestList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                List<BE.MentorListBE> newList = new List<BE.MentorListBE>();
                int Id = Convert.ToInt32(cookie.Values["ID"]);
                newList = new LoginDAL().getMentorList(Id);
                //ViewBag.List = newList;
                if (newList.Count == 0)
                {
                    ViewBag.List = null;
                }
                else
                {
                    ViewBag.List = newList;
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult DeleteMentor(int id)   //id is the clicked Mentor ID
        {
            //ViewBag.Id = id;

            int MentorID = id;

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MenteeID = 0;
            if (cookie != null)
            {
                MenteeID = Convert.ToInt32(cookie.Values["ID"]);
            }
            bool flag = new MemberDAL().DeleteMentor(MentorID, MenteeID);
            return RedirectToAction("Dashboard");
        }
        public ActionResult BookingList()
        {
            return View();
        }

        public ActionResult Favoritres()
        {
            return View();
        }

        public ActionResult SearchMentor()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                int MemberId = Convert.ToInt32(cookie.Values["ID"]);
                SearchMentorBE searchmentor = new SearchMentorBE();
                searchmentor.CareerLevelList = new SearchMentoreDropDown().GetCareerLevelList();
                searchmentor.LoginedMemberId = MemberId;
                //searchmentor.CareerLevelList.
                //searchmentor.DomainList = new SearchMentoreDropDown().GetDomainList();
                // searchmentor.CategoryList = new SearchMentoreDropDown().GetCategoryList();
                //searchmentor.SubCategoryList = new SearchMentoreDropDown().GetSubCategoryList();
                // searchmentor.FilteredMentorList = new MentorSearchDAL().GetFilteredMentorList("",-1,-1,-1,-1,-1,-1);
                return View(searchmentor);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public JsonResult GetMeCareerList()
        {
            SearchMentorBE sm = new SearchMentorBE();
            sm.CareerLevelList = new SearchMentoreDropDown().GetCareerLevelList();

            return Json(sm.CareerLevelList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoadMoreMentorList(string mgender, int mcareerid, int mdomianid, int mcategoryid, int msubcategoryid, int min, int max)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            SearchMentorBE lmm = new SearchMentorBE();
            lmm.FilteredMentorList = new MentorSearchDAL().GetFilteredMentorList(ID, mgender, mcareerid, mdomianid, mcategoryid, msubcategoryid, min, max);


            return Json(lmm.FilteredMentorList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMeDomainList(int CurrentCareerLevelId)
        {
            SearchMentorBE smd = new SearchMentorBE();
            smd.DomainList = new SearchMentoreDropDown().GetDomainList(CurrentCareerLevelId);

            return Json(smd.DomainList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMeCategoryList(int CurrentDomainId)
        {
            SearchMentorBE smc = new SearchMentorBE();
            smc.CategoryList = new SearchMentoreDropDown().GetCategoryList(CurrentDomainId);

            return Json(smc.CategoryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMeSubCategoryList(int CurrentCategoryId)
        {
            SearchMentorBE smsc = new SearchMentorBE();
            smsc.SubCategoryList = new SearchMentoreDropDown().GetSubCategoryList(CurrentCategoryId);

            return Json(smsc.SubCategoryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMeMentorList(string mgender, int mcareerid, int mdomianid, int mcategoryid, int msubcategoryid, int min, int max)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            SearchMentorBE ml = new SearchMentorBE();
            ml.FilteredMentorList = new MentorSearchDAL().GetFilteredMentorList(ID, mgender, mcareerid, mdomianid, mcategoryid, msubcategoryid, min, max);
            // ml.FilteredMentorList = new MentorSearchDAL().GetFilteredMentorList("male", 2, 1, 1, 1, 0, 1000);
            return Json(ml.FilteredMentorList, JsonRequestBehavior.AllowGet);
        }
        public void SendRequestFollowUnfollow(string mentorID)
        {

            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MenteeID = Convert.ToInt32(cookie.Values["ID"]);
            FollowUnfollowBE Follow_unfollow = new FollowUnfollowBE();
            Follow_unfollow.MenteeID = MenteeID;
            Follow_unfollow.MentorID = Convert.ToInt32(mentorID);
            new MenteeMentorFollowshipDAL().Send_Request_FollowShip(Follow_unfollow);
        }
        public ActionResult UnfollowMentor(int id)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int MenteeID = Convert.ToInt32(cookie.Values["ID"]);


            int MentorID = id;

            bool flag = new MentorSearchDAL().DeleteMentor(MenteeID, MentorID);

            return RedirectToAction("SearchMentor");
        }
        public ActionResult BookMentor(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public void Add_Booking(int schid, int slid, int menid, string meetingdate, string tamount)
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            int ID = Convert.ToInt32(cookie.Values["ID"]);
            AddBookingBE addBookingBE = new AddBookingBE();
            addBookingBE.MemberMeetingScheduleId = schid;
            addBookingBE.MemberMeetingSlotId = slid;
            addBookingBE.Mentorid = menid;
            addBookingBE.Menteeid = ID;
            addBookingBE.MeetingDate = meetingdate;
            addBookingBE.TotalAmount = tamount;
            new AddBookingDAL().Insert_Member_Booking(addBookingBE);
        }


        public JsonResult GetSelectedMember(int memberid)
        {
            GetMemberBE member = new GetMemberBE();
            member.SelectedMember = new MentorSearchDAL().GetSelectedMember(memberid);
            return Json(member.SelectedMember, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSlots(int memberid)
        {
            ScheduleTimingsBE allslots = new ScheduleTimingsBE();
            allslots.SlotsList = new ScheduleTimingsDAL().GetAllMeetingSlots(memberid);
            return Json(allslots.SlotsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMeeting_Detail(int sch, int sl, int menid)
        {
            AddBookingBE detail = new AddBookingBE();
            detail.Detail_Meeting = new AddBookingDAL().Gett_Booking_Detail(sch, sl, menid);
            return Json(detail.Detail_Meeting, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PaymentToBook(int mentorid, int schid, int slid, string d)
        {
            ViewBag.MentorId = mentorid;
            ViewBag.ScheduleId = schid;

            ViewBag.Mdate = d;
            ViewBag.SlotId = slid;
            return View();
        }



        public ActionResult AddPayment(String memberID, String PayM, String Tran, String amount)
        {
            PaymentBE payment = new PaymentBE();
            payment.MemberId = Convert.ToInt32(memberID);
            payment.PayM = Convert.ToInt32(PayM);
            payment.Tran = Tran;
            payment.amount = Convert.ToInt32(amount);
            new PaymentDAL().addMemberPayment(payment);

            return View();
        }

        public ActionResult BookingConfirmation()
        {
            return View();
        }

        public ActionResult BookingInvoice()
        {
            return View();
        }
        public ActionResult SearchTraining()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            if (cookie != null)
            {
                int MemberId = Convert.ToInt32(cookie.Values["ID"]);
                SearchMentorBE searchmentor = new SearchMentorBE();
                searchmentor.CareerLevelList = new SearchMentoreDropDown().GetCareerLevelList();
                searchmentor.LoginedMemberId = MemberId;
                //searchmentor.CareerLevelList.
                //searchmentor.DomainList = new SearchMentoreDropDown().GetDomainList();
                // searchmentor.CategoryList = new SearchMentoreDropDown().GetCategoryList();
                //searchmentor.SubCategoryList = new SearchMentoreDropDown().GetSubCategoryList();
                // searchmentor.FilteredMentorList = new MentorSearchDAL().GetFilteredMentorList("",-1,-1,-1,-1,-1,-1);
                return View(searchmentor);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public JsonResult FilteredTrainingList(int mcareerid, int mdomianid, int mcategoryid, int msubcategoryid, int min, int max)
        {
            //HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            //int MentorId = Convert.ToInt32(cookie.Values["ID"]);
            MentorTraining training = new MentorTraining();
            training.TrainingList = new MentorTrainingDAL().TrainingList(mcareerid, mdomianid, mcategoryid, msubcategoryid, min, max);
            foreach (var item in training.TrainingList)
            {
                item.StartDate = item.TrainingStartDate.ToShortDateString();
            }
            return Json(training.TrainingList, JsonRequestBehavior.AllowGet);
        }
        //payment training page
        public ActionResult PaymentTraining(String TrainingId)
        {
            ViewBag.Trainingid = TrainingId;
            return View();

        }
        //title show in training book page
        public JsonResult traininginfo(string trainingid)
        {
            MentorTraining member = new MentorTraining();
            member = new MentorTrainingDAL().GetTrainingView(trainingid);
            return Json(member, JsonRequestBehavior.AllowGet);
        }
        //Day and hourperday function
        public JsonResult trainingDay(string trainingid)
        {
            MentorTraining member = new MentorTraining();
            member = new MentorTrainingDAL().GetTrainingDay(trainingid);
            return Json(member, JsonRequestBehavior.AllowGet);
        }
    }
}
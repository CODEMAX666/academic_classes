using Mentor.BE;
using Mentor.DAL;
using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult RegisterNewMember()
        {
            RegisterBE register = new RegisterBE();
            register.MemberLevelList = new MemberDAL().GetCareerLevelList();
            register.CareerLevelForMenteeSelectionList = new DropDownPopulate().GetCareerLevelForMenteeList();
            return View(register);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterNewMember(MemberBE memberbe)
        {
            RegisterBE register = new RegisterBE();
            register.MemberLevelList = new MemberDAL().GetCareerLevelList();
            register.CareerLevelForMenteeSelectionList = new DropDownPopulate().GetCareerLevelForMenteeList();
            try
            {
                if (Request.Form["CountryCode"] != null)
                {
                    string CountryCode = Request.Form["CountryCode"].ToString();
                    memberbe.PhoneNo = CountryCode + memberbe.PhoneNo;
                }
                if (Request.Form["MemberPossibleMentee"] != null || !string.IsNullOrWhiteSpace(Request.Form["MemberPossibleMentee"]))
                {
                    string MemberPossibleMentee = Request.Form["MemberPossibleMentee"].ToString();


                    //member.MemberLevelList = new MemberDAL().GetCareerLevelList();
                    //member.MemberDomainList = new MemberDAL().GetDomainList(Convert.ToInt32(member.MemberLevel));
                    //memberbe.CareerLevelForMenteeSelectionList = new MemberDAL().GetCareerLevelForMenteeList();

                    if (!string.IsNullOrEmpty(MemberPossibleMentee))
                    {
                        string[] MemberMenteeList = MemberPossibleMentee.Split(',');
                        memberbe.MemberMenteeCareerLevelList = new List<MemberMenteeCareerLevel>();
                        foreach (var id in MemberMenteeList)
                        {
                            MemberMenteeCareerLevel memberMentee = new MemberMenteeCareerLevel();
                            memberMentee.MemberCareerLevelId_fk = Convert.ToInt32(id);
                            memberbe.MemberMenteeCareerLevelList.Add(memberMentee);
                        }

                    }
                }
                //string orignalpassword = member.Password;
                //byte[] encData_byte = new byte[member.Password.Length];
                //encData_byte = System.Text.Encoding.UTF8.GetBytes(member.Password);
                //string encodedData = Convert.ToBase64String(encData_byte);
                //member.Password = encodedData;
                MemberDAL md = new MemberDAL();
                //new MemberDAL().Insert_Update_Member(memberbe);
                md.Insert_Update_Member(memberbe);
                ViewBag.Validation = "True";

                string s = md.getmsg();
                ViewBag.msg = s;
                if (s != "")
                {
                    return View(register);
                }
                else
                {
                    return RedirectToAction("Login", "Member");
                }
            }
            catch(Exception e)
            {
                ViewBag.Validation = "False";
                ViewBag.msg = new MemberDAL().getmsg();
                return View(register);
            }
        }
    }
}
using Mentor.BE;
using Mentor.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class SearchMentorController : Controller
    {
        // GET: SearchMentor
        DropDownPopulate dp = new DropDownPopulate();
        public ActionResult carrerlist()
        {
            /* SearchMentorBE searchmentor = new SearchMentorBE();
             searchmentor.CareerLevelList = new DropDownPopulate().GetCareerLevelList();
             // register.DomainList = new DropDownPopulate().GetDomainList();
             searchmentor.CareerLevelForMenteeSelectionList = new DropDownPopulate().GetCareerLevelForMenteeList();
             return View(searchmentor);*/
            //SearchMentorBE searchmentor = new SearchMentorBE();
            //searchmentor.CareerLevelList = new SelectedCareerList(dp.GetCareerLevelListSearch(),"Value","Text");
            RegisterBE register = new RegisterBE();
            register.MemberLevelList = new DropDownPopulate().GetCareerLevelList();
            return View(register);


        }
    }
}
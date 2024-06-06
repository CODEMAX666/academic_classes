using Mentor.DAL;
using Mentor.BE;
using System;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class PaymentController : Controller
    {
        //MentorMineEntities Entity = new MentorMineEntities();
        // GET: Payment
        public ActionResult Add_Payment()
        {
            return View();
        }
        public ActionResult Payment_Summary()
        {
            return View();
        }
        public JsonResult MySumData()
        {
            //HttpCookie cookie = HttpContext.Request.Cookies["MemberCookies"];
            //int ID = Convert.ToInt32(cookie.Values["ID"]);
            PaymentSumBE paymentSum = new PaymentSumBE();
            paymentSum.PaymentList = new PaySumDAL().MyPaymentSummaries(19);

            return Json(paymentSum.PaymentList, JsonRequestBehavior.AllowGet);


        }
        public ActionResult AddedPayment(String memberID, String PayM, String Tran, String amount)
        {


            PaymentBE payment = new PaymentBE();
            payment.MemberId = Convert.ToInt32(memberID);
            payment.PayM = Convert.ToInt32(PayM);
            payment.Tran = Tran;
            payment.amount = Convert.ToInt32(amount);
            new PaymentDAL().addMemberPayment(payment);
            /*   try
               {
                   if (PayM == "4")
                   { return "OK"; }
                   else

               {
                       return "-1";
                   }


                   new PaymentDAL().addMemberPayment(payment);
                   return View(payment);
               }
               catch (Exception ex)
               {
                   return "-1" + ex.Message;
               }
           }*/
            return View();
        }
        //public String PaymentSu(String Opti)
        //{
        //    int tran = Convert.ToInt32(Opti);
        //    Entity.SP_MemberPaymentProblem_Insert(19, tran, 3, "asd");
        //    Entity.SaveChanges();
        //    return "";
        //}

    }
}
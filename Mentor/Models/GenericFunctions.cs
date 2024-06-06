using Mentor.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mentor.Models
{
    public class GenericFunctions
    {
        public void SetCookie(MemberBE member)
        {
            HttpCookie MemberCookie = new HttpCookie("MemberCookies");
            MemberCookie.Values["UserName"] = member.Email.ToString();
            MemberCookie.Values["Password"] = member.Password.ToString();
            MemberCookie.Values["ID"] = member.MemberId.ToString();
            HttpContext.Current.Response.SetCookie(MemberCookie);
        }
        public void ExpireCookieUserLogin()
        {
            HttpCookie MemberCookie = HttpContext.Current.Request.Cookies["MemberCookies"];

            if (MemberCookie != null)
            {
                HttpCookie Cookie = new HttpCookie("MemberCookies");
                Cookie.Expires = DateTime.Now;
                HttpContext.Current.Response.Cookies.Set(Cookie);
            }

        }

    }
}
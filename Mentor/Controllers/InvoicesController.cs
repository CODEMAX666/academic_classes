using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult InvoicesList()
        {
            return View();
        }

        public ActionResult ViewInvoice()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
   public class PaymentSumBE
    {
       public List<MyPaymentsummary> PaymentList { get; set; }
    }

    public class MyPaymentsummary
    {
        public string PaymentMethod { get; set; }
        public int TransactionID { get; set; }
        public int Amount  { get; set; }

        public string Status { get; set; }
        public string PaymentType { get; set; }            
    }
}

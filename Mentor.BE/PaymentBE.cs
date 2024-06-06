using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class PaymentBE
    {
    
        public int MemberId { get; set; }
        public int PayM { get; set; }
        [MaxLength(20)]
        public String Tran { get; set; }

        public int amount { get; set; }

        public int PaymentTypeID { get; set; }


        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }

       
    }
   
}

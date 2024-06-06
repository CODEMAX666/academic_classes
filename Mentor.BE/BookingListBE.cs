using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class BookingListBE
    {
        public string BookingID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Day { get; set; }
        public string scheduleDate { get; set; }
        public string scheduleStartTime { get; set; }
        public string scheduleEndTime { get; set; }
        public string PhotoURL { get; set; }

        public string Amount { get; set; }
   

        public BookingListBE(string id,string name,string email,string day,string date,string starttime,string endtime,string picUrl,string amount)
        {
            this.BookingID = id;
            this.Name = name;
            this.Email = email;
            this.Day = day;
            this.scheduleDate = date;
            this.scheduleStartTime = starttime;
            this.scheduleEndTime = endtime;
            this.PhotoURL = picUrl;
            this.Amount = amount;
        }

    }
}

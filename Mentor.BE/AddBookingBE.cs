using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class AddBookingBE
    {
        public List<Booking_Detail> Detail_Meeting { get; set; }
        public int MemberMeetingScheduleId { get; set; }
        public int MemberMeetingSlotId { get; set; }
        public int Menteeid { get; set; }
        public int Mentorid { get; set; }
        public string MeetingDate { get; set; }
        public string TotalAmount{ get; set; }
    }
    public class Booking_Detail
    {
        public string Name { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }

        public string Duration { get; set; }
        public string TotalAmount { get; set; }

    }

}

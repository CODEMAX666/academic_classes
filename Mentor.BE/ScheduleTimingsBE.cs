using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class ScheduleTimingsBE
    {
        public List<SelectedScheduleList> ScheduleTimingList { get; set; }
        public List<SelectedSlotsList> SlotsList { get; set; }
    }
    public class SelectedScheduleList
    {
        public int MemberMeetingScheduleId { get; set; }
        public int MemberId { get; set; }
        public string MeetingDay { get; set; }
        public string MeetingStartTime { get; set; }
        public string MeetingEndTime { get; set; }
        public int MeetingDuration { get; set; }
    }
    public class SelectedSlotsList
    {
        public int MemberMeetingScheduleId { get; set; }
        public int MemberMeetingSlotId { get; set; }

        public string SlotDay { get; set; }
        public int MemberId { get; set; }
        public string Date { get; set; }
        //public DateTime EndTime { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public string isReserved { get; set; }
        public string TrainingId { get; set; }
    }
}
//string input = "22:45";

//var timeFromInput = DateTime.ParseExact(input, "H:m", null, DateTimeStyles.None);

//string timeIn12HourFormatForDisplay = timeFromInput.ToString(
//    "hh:mm:ss tt",
//    CultureInfo.InvariantCulture);

//var timeInTodayDate = DateTime.Today.Add(timeFromInput.TimeOfDay);

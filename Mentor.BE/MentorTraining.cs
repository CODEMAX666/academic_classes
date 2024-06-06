using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class MentorTraining
    {
        public List<MentorTraining> TrainingList { get; set; }
        public int MentorTrainingId { get; set; }
        public int MentorId { get; set; }
        public string CareerLevel { get; set; }
        public string Domain { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public List<SelectedList> DomainList { get; set; }
        public List<SelectedList> CareerLevelList { get; set; }
        public List<SelectedList> CategoryList { get; set; }
        public List<SelectedList> SubCategoryList { get; set; }
        public string Title { get; set; }
        public string[] SelectedExperience { get; set; }
        public string Experience { get; set; }
        public List<SelectedList> ExperienceList { get; set; }
        public string Duration { get; set; }
        public List<SelectedList> DurationList { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Day { get; set; }
        public string TrainingPerHourRate { get; set; }
        public string TotalHours { get; set; }
        public string HoursPerDay { get; set; }
        public DateTime TrainingStartDate { get; set; }
        public string StartDate { get; set; }
        public bool IsPublic { get; set; }
        public string DurationName { get; set; }
        public string MemberName { get; set; }
        public string MentorName { get; set; }
        public string Price { get; set; }
    }
}

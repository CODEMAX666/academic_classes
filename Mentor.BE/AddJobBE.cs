using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{


    public class AddJobBE
    {

        public int JobID { get; set; }
        public int MemberId { get; set; }
        public int MemberCareerLevel { get; set; }
        public int MemberDomain { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string ComEmail { get; set; }
        public string City { get; set; }
        public int RequiredExperience { get; set; }
        public string JobKPIS { get; set; }
        public string AppClosingDate { get; set; }
        public int IsPublic { get; set; }
    }
    public class add_JobBE
    {
        public List<SelectedExpList> CareerLevelList { get; set; }
    }
    public class SelectedExpList
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

}


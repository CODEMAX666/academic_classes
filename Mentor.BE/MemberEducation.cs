using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class MemberEducation
    {
        public MemberEducation()
        {
            DegreeLevelList = new List<SelectedList>();
            DegreeTitleList = new List<SelectedList>();
        }
        public List<MemberEducation> MemberEducationList { get; set; }
        public int MemberEducationId { get; set; }
        public int MemberID { get; set; }
        public float Percentage { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public string Institute { get; set; }
        public string DegreeLevelName { get; set; }
        public string DegreeTitleName { get; set; }
        public string DegreeLevel { get; set; }
        public string DegreeTitle { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }

        public List<SelectedList> DegreeLevelList { get; set; }
        public List<SelectedList> DegreeTitleList { get; set; }
       
    }
    
}

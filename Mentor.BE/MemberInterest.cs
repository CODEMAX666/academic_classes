using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class MemberInterest
    {
        public List<MemberInterest> MemberInterestList { get; set; }
        public int MemberInterestId { get; set; }
        public int MemberId { get; set; }
        public string MemberCategory { get; set; }
        public string MemberSubCategory { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }

        public List<SelectedList> MemberCategoryList { get; set; }
        public List<SelectedList> MemberSubCategoryList { get; set; }
        
    }
}

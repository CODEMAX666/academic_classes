using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class MemberExperience
    {
        public MemberExperience()
        {
            MemberCategoryList = new List<SelectedList>();
            MemberSubCategoryList = new List<SelectedList>();
        }
        public List<MemberExperience> MemberExperienceList { get; set; }
        public int MemberExperienceId { get; set; }
        public int MemberId { get; set; }
        public string MemberCategory { get; set; }
        public string MemberSubCategory { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string YearFrom { get; set; }
        public string YearTo { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public string[] MemberSubCategoryArray { get; set; }
        public List<Category> SubCategoryList { get; set; }
        public List<SelectedList> MemberCategoryList { get; set; }
        public List<SelectedList> MemberSubCategoryList { get; set; }
        
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
    }
}

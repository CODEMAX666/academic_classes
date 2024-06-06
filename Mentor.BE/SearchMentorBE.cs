using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class SearchMentorBE
    {
        public List<SelectedList1> CareerLevelList { get; set; }
        public List<SelectedListDomain1> DomainList { get; set; }
        public List<SelectedListCategory1> CategoryList { get; set; }
        public List<SelectedListSubCategory1> SubCategoryList { get; set; }

        public List<SelectedFilteredMentorList> FilteredMentorList { get; set; }
        public string Domain { get; set; }
        public string CareerLevel { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int CareerLevelId { get; set; }
        public int DomainId { get; set; }
        public int LoginedMemberId { get; set; }
    }
    public class SelectedList1
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public class SelectedListDomain1
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public int CareerLevelId { get; set; }
    }
    public class SelectedListCategory1
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public int DomainId { get; set; }
    }
    public class SelectedListSubCategory1
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
    }

    public class SelectedFilteredMentorList
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string Membergender { get; set; }
        public string MemberCareerLevel { get; set; }
        public string MemberDomain { get; set; }
        public string MemberCategory { get; set; }
        public string MemberSubCategory { get; set; }
        //  public float MemberRate { get; set; }
        public string MemberRate { get; set; }

        public string GetStatus { get; set; }
        public string MemberPhotoUrl { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class JobListBE
    {
        public List<MyJobList> jobLists { get; set; }
    }

    public class MyJobList
    {// abdul sami
        public int MarketJobID { get; set; }
        public int MemberId { get; set; }
        public int MemberDomainId { get; set; }
        public string MemberDomainName { get; set; }
        public int MemberCareerLevelId { get; set; }
        public string MemberCareerLevelName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        
        public string Title { get; set; }
        public string Company { get; set; }
        public string CompanyEmail { get; set; }
        public string RequiredExperience { get; set; }
        public string Job_Kpis { get; set; }
        public string City { get; set; }
        public string AppClosingDate { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
   public class ProfileBE
    {
        public List<ProfileBE> ProfileList { get; set; }
        public int MenteeJobrefID { get; set; }
        public int JobID { get; set; }
        public int MemberID { get; set; }
        public int ViewerID { get; set; }
        public int StatusID { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string DateofJoin { get; set; }
        public string CareerLevelName { get; set; }
        public string DomainName { get; set; }
        public string PerHourRate { get; set; }
        public string Photo { get; set; }
        public string AboutYourself { get; set; }
       
        
        public string DegreeLevel { get; set; }
        public string DegreeTitle { get; set; }
        public string Institute { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string TotalExperience { get; set; }
        public string ExpCategory { get; set; }
        public string ExpSubCat { get; set; }
        public string InterestCategory { get; set; }
        public string InterestSubCat { get; set; }

        //public string JobCompany { get; set; }
        //public string JobCity { get; set; }
        //public string ClosingDate { get; set; }
        //public string JobTitle { get; set; }
        //public string Address { get; set; }
        //public string Country { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string PostalCode { get; set; }
    }
}

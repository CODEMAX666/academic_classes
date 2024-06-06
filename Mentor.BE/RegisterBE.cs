using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class RegisterBE
    {
        public RegisterBE()
        {
            MemberDomainList = new List<SelectedList>();
            MemberLevelList = new List<SelectedList>();
            CareerLevelForMenteeSelectionList = new List<SelectedList>();

        }
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string SecondaryPhoneNo { get; set; }
        public string MemberDomain { get; set; }
        public string MemberLevel { get; set; }
        public bool IsMentor { get; set; }
        public bool IsDeleted { get; set; }
        public string MemberCurrentRate { get; set; }
        public string MemberPossibleMentee { get; set; }
        public string[] MemberPossibleMenteeArray { get; set; }
        public string AboutYourSelf { get; set; }
        public bool IsMentorFollowLimitMeet { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
        public List<MemberSelectedDomain> MemberSelectedDomainList { get; set; }
        public List<SelectedList> CareerLevelForMenteeSelectionList { get; set; }
        public List<MemberMenteeCareerLevel> MemberMenteeCareerLevelList { get; set; }
        public List<SelectedList> MemberDomainList { get; set; }
        public List<SelectedList> MemberLevelList { get; set; }
    }

    public class SelectedListDomain
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public int CareerLevelId { get; set; }
    }
   
}

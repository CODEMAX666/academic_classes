using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class MentorPackageBE
    {
        public List<MentorPackageBE> MentorPackageList { get; set; }
        public string MentorOwnPackageId { get; set; }
        public int MenteePackageId { get; set; }
        public int MentorId { get; set; }
        public string MenteePackageName { get; set; }
        public string MemberMenteeCareerLevel { get; set; }
        public string MenteePackageDescription { get; set; }
        public string PackageRate { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }

        public List<SelectedList> MemberMenteeCareerLevelList { get; set; }
        public List<PackageList> MenteePackageNameList { get; set; }
    }
    public class PackageList
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public string MenteePackageDescription { get; set; }
    }
}

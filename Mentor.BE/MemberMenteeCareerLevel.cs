using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class MemberMenteeCareerLevel
    {
        public int MemberMenteeCareerLevelId { get; set; }
        public int MemberId_fk { get; set; }
        public int MemberCareerLevelId_fk { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}

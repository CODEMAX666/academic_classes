using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    /// <summary>
    /// MemberCareerLevel
    /// </summary>
    public class MemberCareerLevelBE
    {
        public int MemberCareerLevelId { get; set; }
        public string MemberCareerLevel { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
    }
}

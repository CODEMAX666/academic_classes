using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class MemberRateBE
    {
        public int MemberRate { get; set; }
        public float PerHourRate { get; set; }
        public bool IsCurrent { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ModifiedDateTime { get; set; }
    }
}

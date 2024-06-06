using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class FollowUnfollowBE
    {
        public int MentorID { get; set; }
        public int MenteeID { get; set; }
        public int FollowStatusID { get; set; }
        public string CreatedTimeDate { get; set; }
        public string ModifiedTimeDate { get; set; }
    }
}

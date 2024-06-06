using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    public class JobRefferListBE
    {
        public List<JobRefferList> JobReffers { get; set; }
    }

    public class JobRefferList
    {
        public int MemberID { get; set; }
        public int ViewerID { get; set; }
        public int StatusID { get; set; }
        public int JobID { get; set; }
        public string MenteeName { get; set; }
        public string JobTitle { get; set; }
        public string RequestDate { get; set; }
        
       
    }
    public class JobRequestListBE
    {
        public List<JobRequestList> JobReffers { get; set; }
    }

    public class JobRequestList
    {
        public string JobName { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
   public class GetMemberBE
    {
        public List<MyMember> SelectedMember = new List<MyMember>();
        
    }
    public class MyMember
    {
       
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CareerLevelName { get; set; }
        public string MemberCurrentRate { get; set; }
        public string PhotoURL { get; set; }
    }
}

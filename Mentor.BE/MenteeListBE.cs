using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
    /// <summary>
    /// MenteeListBE
    /// </summary>
    public class MenteeListBE
    {


        //public List<MenteeListBE> getMenteeList = new List<MenteeListBE>();
        //public string Id { get; set; }

        public string MenteeID { get; set; }
        public string Name { get; set; }
        public string PerHourRate { get; set; }
        public string PhoneNum { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public string Status { get; set; }

        //public List<SelectedList> StatusList { get; set; }


        public MenteeListBE(string id, string name, string perHourRate, string phoneNum, string catName, string subCatName, string status /*,List<SelectedList> newList*/)
        {
            this.MenteeID = id;
            Name = name;
            PerHourRate = perHourRate;
            PhoneNum = phoneNum;
            this.CategoryName = catName;
            this.SubCategoryName = subCatName;
            this.Status = status;
            //this.StatusList = newList;
        }
    }
}

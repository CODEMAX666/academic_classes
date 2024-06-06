using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.DAL
{
    public class DataUtil
    {
        public static string connectionString;
        
        static DataUtil()
        {
            DataUtil.connectionString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        }
    }
}

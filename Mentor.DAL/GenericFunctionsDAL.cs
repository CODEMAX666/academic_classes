using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.DAL
{
    public class GenericFunctionsDAL
    {
        public int? checknullint(int num)
        {
            if (num == -1)
            {
                return null;
            }
            else
            {
                return num;
            }
        }
        public float? checknullfloat(float num)
        {
            if (num == -1)
            {
                return null;
            }
            else
            {
                return num;
            }
        }
    }
}

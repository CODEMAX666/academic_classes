using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.BE
{
            
        public class TrainingsListBE
        {
            public List<MyTrainingList> myTrainings { get; set; }
        }
    /// <summary>
    /// MyTrainingList
    /// </summary>
    public class MyTrainingList
        {
            public string TaskTitle { get; set; }
            public string StartingDate { get; set; }
            public string TrainingHour { get; set; }
           
        }

    }

